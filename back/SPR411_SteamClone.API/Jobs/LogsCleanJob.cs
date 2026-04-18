using Quartz;

namespace SPR411_SteamClone.API.Jobs
{
    public class LogsCleanJob : IJob
    {
        private readonly IWebHostEnvironment _env;

        public LogsCleanJob(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var logsPath = Path.Combine(_env.ContentRootPath, "Logs");

            var files = Directory.GetFiles(logsPath);

            foreach (var filePath in files)
            {
                var file = new FileInfo(filePath);
                
                if(DateTime.Now - file.CreationTime > TimeSpan.FromMinutes(7))
                {
                    file.Delete();
                }
            }
        }
    }
}
