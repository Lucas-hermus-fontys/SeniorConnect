using SeniorConnect.Commands;

namespace SeniorConnect.Util
{
    public class CommandUtil
    {
        private readonly MigrationCommand _migrationCommand = new MigrationCommand();
        public void RunAsCommand(String[] args)
        {
            switch (args[0])
            {
                case "migration":
                    _migrationCommand.MigrateDatabase();
                    break;
                default:
                    InvalidCommand();
                    break;
            }
        }

        public void InvalidCommand(String message = "Invalid command")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }


}