using Domain.Commands;
using Server.Domain.Util;

namespace Domain.Util
{
    public class CommandUtil
    {
        private readonly MigrationCommand _migrationCommand;
        private readonly TestCommand _testCommand;

        public CommandUtil(MigrationCommand migrationCommand, TestCommand testCommand)
        {
            _migrationCommand = migrationCommand;
            _testCommand = testCommand;
        }
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