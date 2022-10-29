using NewLogger;
using NewLogger.Services;

var fileService = new FileService();
var logger = new Logger(fileService);
var action = new Actions(logger);

logger.NeedBackup += async () => await fileService.BackupAsync();

for (int i = 0; i < 2; i++)
{
    await Task.Run(() => Randomise());
}

await fileService.BackupAsync();

void Randomise()
{
    var rand = new Random();
    for (int i = 0; i < 50; i++)
    {
        int random = rand.Next(3);
        if (random == 0)
        {
            action.Method1();
        }
        else if (random == 1)
        {
            action.Method2();
        }
        else
        {
            action.Method3();
        }
    }
}