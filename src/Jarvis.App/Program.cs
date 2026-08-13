using Jarvis.App;
using Jarvis.App.Commands;
using Jarvis.App.Settings;
using Jarvis.App.Services;
Console.WriteLine("Jarvis 0.3");
Console.WriteLine();

SettingsService settingsService = new SettingsService();

AppSettings settings = settingsService.Load();

List<ICommand> commands = new List<ICommand>();

commands.Add(new TimeCommand());
commands.Add(new DateCommand());
commands.Add(new EchoCommand());
commands.Add(new OpenCommand());
commands.Add(new SearchCommand(settings));
commands.Add(new SearchEngineCommand(settings, settingsService));
commands.Add(new ExitCommand());
commands.Add(new HelpCommand(commands));

CommandHandler commandHandler = new CommandHandler(commands);

bool isRunning = true;

while (isRunning)
{
    Console.Write("> ");

    string? command = Console.ReadLine();

    if (command == null)
    {
        continue;
    }

    command = command.Trim();

    isRunning = commandHandler.Handle(command);
}