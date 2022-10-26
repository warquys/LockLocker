using CommandSystem;
using System;

namespace LockLocker;

public class UnlockCommand : ICommand
{
    public string Command { get; } = "Unlock";

    public string[] Aliases { get; } = new[] { "ul", "unl", "ulock" };

    public string Description { get; } = "Unlock all of the locker of the map";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Plugin.Instance.LockerLock = false;

        response = $"All locker are unlock";

        return true;
    }
}

