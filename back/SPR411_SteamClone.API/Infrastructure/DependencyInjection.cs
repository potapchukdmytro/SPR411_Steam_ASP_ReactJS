using Quartz;
using SPR411_SteamClone.API.Jobs;

namespace SPR411_SteamClone.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJobs(this IServiceCollection services, params (Type type, string cron)[] jobs)
        {
            services.AddQuartz(q =>
            {
                foreach (var job in jobs)
                {
                    var jobKey = new JobKey(job.type.Name);
                    q.AddJob(job.type, jobKey);

                    q.AddTrigger(cfg => cfg
                        .ForJob(jobKey)
                        .WithIdentity($"{job.type.Name}-trigger")
                        .WithCronSchedule(job.cron));
                }
            });

            return services;
        }
    }
}
