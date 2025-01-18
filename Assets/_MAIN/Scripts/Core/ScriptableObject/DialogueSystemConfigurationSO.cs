using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Configuration Asset", menuName = "Dialogue System/Dialogue Configuration Asset")]
public class DialogueSystemConfigurationSO : ScriptableObject
{
    public CharacterConfigSO CharacterConfigAsset;

    public Color defaultTextColor = Color.white;
    public TMP_FontAsset defaultfontAsset;
}