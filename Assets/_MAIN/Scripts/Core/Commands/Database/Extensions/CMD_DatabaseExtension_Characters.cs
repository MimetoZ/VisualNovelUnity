using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMD_DatabaseExtension_Characters : CMD_DatabaseExtension
{
    new public static void Extend(CommandDatabase database)
    {
        database.AddCommand("show", new Func<string[], IEnumerator>(ShowAll));
        database.AddCommand("hide", new Func<string[], IEnumerator>(HideAll));
    }

    public static IEnumerator ShowAll(string[] data)
    {
        List<Character> characters = new List<Character>();

        foreach (string s in data)
        {
            Character character = CharacterManager.instance.GetCharacter(s);
            if (character != null )
                characters.Add(character);
        }

        if (characters.Count == 0)
            yield break;

        foreach (Character character in characters) 
            character.Show();
    }

    public static IEnumerator HideAll(string[] data)
    {
        List<Character> characters = new List<Character>();

        foreach (string s in data)
        {
            Character character = CharacterManager.instance.GetCharacter(s);
            if (character != null)
                characters.Add(character);
        }

        if (characters.Count == 0)
            yield break;

        foreach (Character character in characters)
            character.Hide();
    }
}
