using Quartz;

namespace SPR411_SteamClone.API.Jobs
{
    public class ConsoleSpamJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"My first quartz job - {DateTime.Now}");
        }
    }
}
