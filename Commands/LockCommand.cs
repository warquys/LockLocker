using CommandSystem;
using System;

namespace LockLocker.Commands;

public class LockCommand : ICommand
{
    public string Command { get; } = "Lock";

    public string[] Aliases { get; } = new[] { "l" };

    public string Description { get; } = "Lock all of the locker of the map";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Plugin.Instance.LockerLock = true;

        response = $"All locker are lock";

        return true;
    }
}
