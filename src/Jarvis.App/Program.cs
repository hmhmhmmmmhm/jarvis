using Jarvis.App;
using Jarvis.App.Commands;
using Jarvis.App.Settings;
using Jarvis.App.Services;
using Jarvis.App.Parsing;
using Jarvis.App.Text;
using Jarvis.App.Models;
Console.WriteLine("Jarvis 0.3");
Console.WriteLine();

SettingsService settingsService = new SettingsService();

AppSettings settings = settingsService.Load();

// Создаём реестр приложений и заполняем его
// стандартными значениями при первом запуске.
ApplicationRegistry applicationRegistry =
    new ApplicationRegistry(settings, settingsService);

applicationRegistry.EnsureDefaults();

List<ICommand> commands = new List<ICommand>();
commands.Add(new WakeWordCommand(settings, settingsService));
commands.Add(new TimeCommand());
commands.Add(new DateCommand());
commands.Add(new EchoCommand());

commands.Add(new OpenCommand());
commands.Add(new SearchEngineCommand(settings, settingsService));
commands.Add(new SearchCommand(settings));
commands.Add(new LaunchCommand(applicationRegistry));

commands.Add(new AppListCommand(applicationRegistry));
commands.Add(new AppAddCommand(applicationRegistry));
commands.Add(new AppRemoveCommand(applicationRegistry));

commands.Add(new ExitCommand());
commands.Add(new HelpCommand(commands));

CommandHandler commandHandler = new CommandHandler(commands);

TextNormalizer textNormalizer = new TextNormalizer();
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

    // Проверяем wake word до того, как нормализатор его удалит.
    bool hasWakeWord = textNormalizer.HasWakeWord(command);

    // Если обязательное обращение включено, без слова "Джарвис" команду игнорируем.
    if (settings.RequireWakeWord && !hasWakeWord)
    {
        continue;
    }

    // Убираем wake word и вежливые слова.
    command = textNormalizer.Normalize(command);

    // IntentParser превращает человеческую фразу
    // в структурированное внутреннее намерение.
    Intent intent = intentParser.Parse(command);

    // CommandHandler получает уже готовые имя команды и аргумент.
    isRunning = commandHandler.Handle(intent);
}