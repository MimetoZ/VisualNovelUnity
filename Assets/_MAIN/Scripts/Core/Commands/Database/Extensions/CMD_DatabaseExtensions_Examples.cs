using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CMD_DatabaseExtensions_Examples : CMD_DatabaseExtension
{
    new public static void Extend(CommandDatabase database)
    {
        //Add Action
        database.AddCommand("print", new Action(PrintDefaultMessage));
        database.AddCommand("print_1p", new Action<string>(PrintUserMesseg));
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));

        //Add lambda
        database.AddCommand("lambda", new Action(() => { Debug.Log("Print a default message to console. - lambda command"); }));
        database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"User Message: '{arg}' - lambda"); }));
        database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); }));

        //Add coroutine
        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
        database.AddCommand("process_1p", new Func<string, IEnumerator>(LineProcess));
        database.AddCommand("process_mp", new Func<string[], IEnumerator>(MultiLineProcess));
    }

    private static void PrintDefaultMessage()
    {
        Debug.Log("Print a default message to console.");
    }
    
    private static void PrintUserMesseg(string message)
    {
        Debug.Log($"User Message: '{message}'");
    }

    private static void PrintLines(string[] lines)
    {
        int i = 1;
        foreach (var line in lines)
        {
            Debug.Log($"{i++}: {line}");
        }
    }

    private static IEnumerator SimpleProcess()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"Process Running... [{i}]");
            yield return new WaitForSeconds(1);
        }
    }

    private static IEnumerator LineProcess(string data)
    {
        if (int.TryParse(data, out int num))
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log($"Process Running... [{i}]");
                yield return new WaitForSeconds(1);
            }
        }
        
    }

    private static IEnumerator MultiLineProcess(string[] data)
    {

        foreach (var line in data)
        {
            Debug.Log($"Process Message... [{line}]");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
