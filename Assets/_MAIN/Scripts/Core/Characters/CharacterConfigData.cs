using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class CharacterConfigData
{
    public string name;
    public string alias;
    public Character.CharacterType type;

    public Color nameColor;
    public Color dialogueColor;

    public TMP_FontAsset nameFont;
    public TMP_FontAsset dialogueFont;

    public CharacterConfigData Copy()
    {
        CharacterConfigData copy = new CharacterConfigData();

        copy.name = name;
        copy.alias = alias;
        copy.type = type;
        copy.nameColor = new Color(nameColor.r, nameColor.g, nameColor.b, nameColor.a);
        copy.dialogueColor = new Color(dialogueColor.r, dialogueColor.g, dialogueColor.b, dialogueColor.a);
        copy.nameFont = nameFont;
        copy.dialogueFont = dialogueFont;

        return copy;
    }

    private static Color defaultColor => DialogueSystem.instance.config.defaultTextColor;
    private static TMP_FontAsset defaultFontAsset => DialogueSystem.instance.config.defaultfontAsset;
    public static CharacterConfigData Default
    {
        get
        {
            CharacterConfigData copy = new CharacterConfigData();

            copy.name = "";
            copy.alias = "";
            copy.type = Character.CharacterType.Text;
            copy.nameColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, defaultColor.a);
            copy.dialogueColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, defaultColor.a);
            copy.nameFont = defaultFontAsset;
            copy.dialogueFont = defaultFontAsset;

            return copy;
        }
    }
}
