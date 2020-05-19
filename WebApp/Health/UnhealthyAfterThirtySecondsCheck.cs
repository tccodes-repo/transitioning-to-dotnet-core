using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace WebApp.Health
{
    public class UnhealthyAfterThirtySecondsCheck : IHealthCheck
    {
        public static readonly DateTime Started = DateTime.Now;
        
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (DateTime.Now.Subtract(Started).TotalSeconds > 30)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("It has been more than 30 seconds"));
            }

            return Task.FromResult(HealthCheckResult.Healthy("Good to go"));
        }
    }
}