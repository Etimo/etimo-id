using Etimo.Id.Api.Security;
using Etimo.Id.Exceptions;
using Etimo.Id.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etimo.Id.Api.Middleware
{
    public class RateLimiterMiddleware
    {
        private static RateLimiterSettings Settings;

        private static readonly IDictionary<string, DateTime> Banned = new ConcurrentDictionary<string, DateTime>();
        private readonly        IDistributedCache             _cache;
        private readonly        RequestDelegate               _next;

        public RateLimiterMiddleware(
            RequestDelegate next,
            IDistributedCache cache,
            RateLimiterSettings settings)
        {
            _next    = next;
            _cache   = cache;
            Settings = settings;
        }

        public async Task Invoke(HttpContext context)
        {
            RateLimiterContext rateLimitContext = await GetRateLimiterContextAsync(context);

            try
            {
                CheckIfCallerIsBanned(rateLimitContext);

                DateTime timeBefore = DateTime.UtcNow;
                await _next(context);
                rateLimitContext.ResponseTime = (int)(DateTime.UtcNow - timeBefore).TotalMilliseconds;
                rateLimitContext.Rules.ForEach(r => ProcessSuccessfulRequest(r));
            }
            catch (TooManyRequestsException) { throw; }
            catch (Exception exception)
            {
                rateLimitContext.Rules.ForEach(r => ProcessFailedRequest(exception, r));
                throw;
            }
            finally { await WriteRateLimiterContextAsync(rateLimitContext); }
        }

        private async Task<RateLimiterContext> GetRateLimiterContextAsync(HttpContext context)
        {
            var ip                 = context.Connection.RemoteIpAddress?.ToString();
            var rateLimiterContext = new RateLimiterContext();
            rateLimiterContext.IpNumber = ip;

            foreach (RateLimiterRule rule in Settings.Rules)
            {
                var                    cacheKey      = $"{rule.Name}:{ip}";
                string                 contextString = await _cache.GetStringAsync(cacheKey);
                RateLimiterRuleContext ruleContext;
                if (contextString != null)
                {
                    ruleContext               = new RateLimiterRuleContext(contextString);
                    ruleContext.ExistsInCache = true;
                    ruleContext.Rule          = Settings.Rules.Find(r => r.Name == rule.Name);
                }
                else { ruleContext = new RateLimiterRuleContext(rule); }

                ruleContext.Context = rateLimiterContext;
                rateLimiterContext.Rules.Add(ruleContext);
            }

            return rateLimiterContext;
        }

        private void CheckIfCallerIsBanned(RateLimiterContext context)
        {
            foreach (RateLimiterRuleContext ruleContext in context.Rules)
            {
                if (ruleContext.Banned) { throw new TooManyRequestsException("Too many requests", ruleContext.SecondsLeftOnBan); }
            }
        }

        private Task WriteRateLimiterContextAsync(RateLimiterContext rateLimiterContext)
        {
            foreach (RateLimiterRuleContext ruleContext in rateLimiterContext.Rules)
            {
                var value    = ruleContext.ToString();
                var cacheKey = $"{ruleContext.Name}:{rateLimiterContext.IpNumber}";
                _cache.SetStringAsync(
                    cacheKey,
                    value,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = ruleContext.WindowExpiration,
                    });
            }

            return Task.FromResult(new object());
        }

        private void ProcessFailedRequest(Exception exception, RateLimiterRuleContext ruleContext)
        {
            RateLimiterRule rule          = ruleContext.Rule;
            string          exceptionName = exception.GetType().Name;
            if (!rule.RateLimitExceptions.Contains(exceptionName))
            {
                ProcessSuccessfulRequest(ruleContext);
                return;
            }

            ruleContext.SoftRequests++;
            ruleContext.HardRequests++;

            // Use HarmlessRequests (i.e. successful requests with low response time)
            // to reduce the value of SoftRequests when comparing to the limit.
            // This way, a caller with many clients won't be banned if there are many
            // users that are failing their requests due to natural reasons.
            int modulatedRequests = ruleContext.SoftRequests - (ruleContext.HarmlessRequests / ruleContext.Rule.SuccessfulToFailedRatio);
            if (modulatedRequests > rule.SoftRequestLimit || ruleContext.HardRequests > rule.HardRequestLimit)
            {
                ruleContext.BannedUntil = DateTime.UtcNow.AddMinutes(rule.BanForMinutes);
            }
        }

        private void ProcessSuccessfulRequest(RateLimiterRuleContext ruleContext)
        {
            if (ruleContext.Context.ResponseTime > ruleContext.Rule.RateLimitResponseMilliseconds)
            {
                ruleContext.SoftRequests++;
                ruleContext.HardRequests++;
            }
            else { ruleContext.HarmlessRequests++; }
        }
    }


    public static class RateLimiterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiter(this IApplicationBuilder builder)
        {
            IConfiguration config              = builder.ApplicationServices.GetService<IConfiguration>();
            var            rateLimiterSettings = new RateLimiterSettings();
            config.GetSection("RateLimiterSettings").Bind(rateLimiterSettings);

            IDistributedCache cache = builder.ApplicationServices.GetService<IDistributedCache>();

            return builder.UseMiddleware<RateLimiterMiddleware>(cache, rateLimiterSettings);
        }
    }
}
