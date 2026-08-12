using Jarvis.App;
using Jarvis.App.Commands;

Console.WriteLine("Jarvis 0.2");
Console.WriteLine();

List<ICommand> commands = new List<ICommand>();

commands.Add(new TimeCommand());
commands.Add(new EchoCommand());
commands.Add(new ExitCommand());
commands.Add(new DateCommand());
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