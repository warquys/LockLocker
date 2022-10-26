using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace LockLocker;

/*
                                          
                                 @@            @@@@@@@
                            @@@@@@   @@@@@       @@@@@@@@@@@
                  @@@      @@@@@   @@@@@@             @@@
               @@@@@@                         @
         @@@  @@@@        
        @@               
       |-|    .-.       
       ] [    | |      _    .-----.
     ."   """"   """""" """"| .--/                  
     |--:--:--:--:--:--:--:-| [___    .------------------------|
     |                       [===]|'='|.    H      H      H   .|
    /|               _------------|'  |                        |
  /  |-----\______.--.______.--._ |---\'--\-.-/==\-.-/==\-.-/-'/
/____;^=(=°)======(=°)======(=°)=^     ^^^^(¤)^^^^(¤)^^^^(¤)^^^
    oui une bonne veille 030 anglais Y14... reste que les 241p sont mieux (241p-17 ❤️)


    CHOU CHOU !! welcome! for a New Coding Challe... wait, what ?...
 */


//effingo de meo codice
internal static class TranspilerFactory
{
    /// <summary>
    /// Given a lambda expression calling a specifique method to get the code of the designated method as a list of <see cref="CodeInstruction"/>
    /// </summary>
    /// <param name="expression">The lambda expression using the method</param>
    /// <returns>The <see cref="CodeInstruction"/> list of the method</returns>
    public static List<CodeInstruction> GetCodeInstruction(Expression<Action> expression)
    {
        var method = SymbolExtensions.GetMethodInfo(expression);

        return PatchProcessor.GetOriginalInstructions(method);
    }

    /// <summary>
    /// Given a lambda expression calling a specifique method to add this code of this method to the code of parm: <paramref name="codes"/>
    /// </summary>
    /// <param name="codes">The curent code</param>
    /// <param name="expression">The lambda expression using the method</param>
    /// <returns>The new <see cref="CodeInstruction"/> list whith the <see cref="CodeInstruction"/> of the <paramref name="expression"/></returns>
    public static List<CodeInstruction> BadPrefix(this IEnumerable<CodeInstruction> codes, Expression<Action> expression)
    {
        var codeToAdd = GetCodeInstruction(expression);

        codeToAdd.RemoveEndCode().RemoveThistagCode().AddRange(codes);

        return codeToAdd;
    }
    //couper

    private static List<CodeInstruction> RemoveEndCode(this List<CodeInstruction> codes)
    {
        var removeNext = false;
        for (int i = codes.Count - 1; i >= 0; i--)
        {
            if (removeNext)
            {
                codes.RemoveAt(i);
                removeNext = false;
                continue;
            }

            if (i < 1) return codes;
            
            if (codes[i].opcode == OpCodes.Throw && codes[i - 1].opcode == OpCodes.Ldnull)
            {
                codes.RemoveAt(i);
                removeNext = true;
                continue;
            }
        }
        return codes;
    }

    private static List<CodeInstruction> RemoveThistagCode(this List<CodeInstruction> codes)
    {
        for (int i = codes.Count - 1; i >= 0; i--)
        {
            var code = codes[i];
            if (code.opcode == OpCodes.Ldfld && code.operand is System.Reflection.FieldInfo rieldInfo && rieldInfo.Name == "this")
            {
                codes.RemoveAt(i);
            }
        }
        return codes;
    }

    //couper
}
