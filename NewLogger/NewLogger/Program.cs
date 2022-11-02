using NewLogger;
using NewLogger.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Тут же задание: что метод мейн подписан на событие класса логер...
        var action = new Actions(new Logger(new FileService(new JsonReader().ReadJsonFile())));

        var list = new List<Task>();
        for (int i = 0; i < 2; i++)
        {
            list.Add(Task.Run(async () =>
            {
                for (int i = 0; i < 50; i++)
                {
                    var rand = new Random();
                    var random = rand.Next(3);
                    switch (random)
                    {
                        case 0:
                            await action.Method1();
                            break;
                        case 1:
                            await action.Method2();
                            break;
                        case 2:
                            await action.Method3();
                            break;
                    }
                }
            }));
        }

        await Task.WhenAll(list);
    }
}