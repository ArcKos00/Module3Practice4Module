using NewLogger;
using NewLogger.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var fileService = new FileService(new JsonReader().ReadJsonFile());
        var logger = new Logger(fileService);
        var action = new Actions(logger);
        logger.NeedBackup += async () => await fileService.BackupAsync();

        await FORERCH();

        async Task FORERCH()
        {
            var list = new List<Task>();
            for (int i = 0; i < 2; i++)
            {
                list.Add(Task.Run(async () =>
                {
                    for (int i = 0; i < 50; i++)
                    {
                        await Randomise(action);
                    }
                }));
            }

            await Task.WhenAll(list);
        }

        async Task Randomise(IActions actions)
        {
            var rand = new Random();
            var random = rand.Next(3);
            switch (random)
            {
                case 0:
                    await actions.Method1();
                    break;
                case 1:
                    await actions.Method2();
                    break;
                case 2:
                    await actions.Method3();
                    break;
            }
        }
    }
}