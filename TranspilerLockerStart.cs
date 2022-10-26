using Exiled.API.Features;
using HarmonyLib;
using Interactables.Verification;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LockLocker
{

    [HarmonyPatch(typeof(LockerChamber), nameof(LockerChamber.CanInteract), MethodType.Getter)]
    static class TranspilerLockerStart
    {
        [HarmonyTranspiler]//On m'a dit que c'était plus rapide, puis que ça ne génée pas les autres patches
        public static IEnumerable<CodeInstruction> Repalce(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var code = new Code_LockerChamber();

            instructions = instructions.BadPrefix(() => code.Code_get_CanInteract());

            return instructions;
        }
    }

    internal partial class Code_LockerChamber
    {
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public bool Code_get_CanInteract()
        {
            if (Plugin.Instance.LockerLock && Plugin.Instance.LockerChambers.Contains(@this))
                return false;

            throw null;
        }

    }
}
