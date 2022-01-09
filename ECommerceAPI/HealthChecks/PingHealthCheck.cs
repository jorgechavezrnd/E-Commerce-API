using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.HealthChecks
{
    public class PingHealthCheck : IHealthCheck
    {
        private readonly string _host;

        public PingHealthCheck(string host)
        {
            _host = host;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var ping = new Ping();
            HealthCheckResult result = HealthCheckResult.Healthy();

            try
            {
                var reply = await ping.SendPingAsync(_host);

                if (reply != null)
                {
                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            result = HealthCheckResult.Healthy("Todo bien!");
                            break;
                        case IPStatus.TimedOut:
                            result = HealthCheckResult.Degraded($"Esta lento el host {_host}");
                            break;
                        default:
                            result = HealthCheckResult.Unhealthy($"Servicio {_host} no funciona!!");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                result = HealthCheckResult.Unhealthy("Error General", e);
            }

            return result;
        }
    }
}
