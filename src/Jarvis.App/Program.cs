using Jarvis.App;
using Jarvis.App.Commands;
using Jarvis.App.Settings;
using Jarvis.App.Services;
using Jarvis.App.Parsing;
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
commands.Add(new WakeWordCommand(settings, settingsService));
commands.Add(new ExitCommand());
commands.Add(new HelpCommand(commands));

CommandHandler commandHandler = new CommandHandler(commands);
IntentParser intentParser = new IntentParser();

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

    // Запоминаем, обратился ли пользователь к Jarvis по имени,
    // пока IntentParser ещё не удалил wake word из строки.
    bool hasWakeWord = intentParser.HasWakeWord(command);

    // Если обязательный wake word включён и имени нет,
    // просто ждём следующую команду.
    if (settings.RequireWakeWord && !hasWakeWord)
    {
        continue;
    }

    // После проверки убираем "Джарвис" и определяем намерение пользователя.
    command = intentParser.Parse(command);

    isRunning = commandHandler.Handle(command);
}