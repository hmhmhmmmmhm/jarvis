using Jarvis.App;

Console.WriteLine("Jarvis 0.1");
Console.WriteLine();

bool isRunning = true;

while (isRunning)
{
    Console.Write("> Введите help для списка команд: ");

    string? command = Console.ReadLine();

    if (command == null)
    {
        continue;
    }

    command = command.Trim();

    isRunning = CommandHandler.Handle(command);
}