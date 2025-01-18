using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CMD_DatabaseExtension
{
    public static void Extend(CommandDatabase database) { }
    public static CommandParameter CommandParameter(string[] data) => new CommandParameter(data);
}
