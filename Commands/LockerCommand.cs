using CommandSystem;
using System;

namespace LockLocker.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class LockerCommand : ParentCommand
{
    public LockerCommand() => LoadGeneratedCommands();

    public override string Command { get; } = "Locker";

    public override string[] Aliases { get; } = { };

    public override string Description { get; } = "Locker Commands";

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new LockCommand());
        RegisterCommand(new UnlockCommand());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(PlayerPermissions.FacilityManagement, out response))
            return false;
        
        response = @"Possible command:
Lock - Lock all of the locker of the map.
    Alias: l
Unlock - Unlock all of the locker of the map.
    Alias: ul, unl, ulock";

        return true;
    }
}
