using SeniorConnect.Domain.Commands;

namespace SeniorConnect.Domain.Util
{
    public class CommandUtil
    {
        private readonly MigrationCommand _migrationCommand = new();
        private readonly TestCommand _testCommand = new();
        public void RunAsCommand(String[] args)
        {
            switch (args[0])
            {
                case "migration":
                    _migrationCommand.MigrateDatabase(args);
                    break;
                case "test":
                    _testCommand.Test(args);
                    break;
                default:
                    LoggingUtil.Log("Invalid command", ConsoleColor.Red);
                    break;
            }
        }
    }


}