using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance { get; private set; }

    private Dictionary<string, Character> characters = new Dictionary<string, Character>();

    private CharacterConfigSO config => DialogueSystem.instance.config.CharacterConfigAsset;

    private const string CHARACTER_CHASTING_ID = " as ";
    private const string CHARACTER_NAME_ID = "<charname>";
    public string characterRootPhath => $"Characters/{CHARACTER_NAME_ID}";
    public string characterPrefabName => $"Character - [{CHARACTER_NAME_ID}]";
    public string characterPrefabPath => $"{characterRootPhath}/{characterPrefabName}";

    [SerializeField] private RectTransform _characterpanel = null;
    public RectTransform characterPanel => _characterpanel;

    private void Awake()
    {
        instance = this;
    }

    public CharacterConfigData GetCharacterConfig(string characterName)
    {
        return config.GetConfig(characterName);
    }

    public Character GetCharacter(string characterName, bool createIfDoesNotExist = false)
    {
        if (characters.ContainsKey(characterName.ToLower()))
            return characters[characterName.ToLower()];
        else if (createIfDoesNotExist)
            return CreateCharacter(characterName);

        return null;
    }

    public Character CreateCharacter(string characterName)
    {
        if (characters.ContainsKey(characterName.ToLower()))
        {
            Debug.LogWarning("Character exists.");
            return null;
        }

        CHARACTER_INF info = GetCharacterInfo(characterName);

        Character character = CreatCharacterFromInfo(info);

        characters.Add(characterName.ToLower(), character);

        character.Show();

        return character;
    }

    private CHARACTER_INF GetCharacterInfo(string characterName)
    {
        CHARACTER_INF result = new CHARACTER_INF();

        string[] name = characterName.Split(CHARACTER_CHASTING_ID, System.StringSplitOptions.RemoveEmptyEntries);

        result.name = name[0];
        result.castingName = name.Length > 1 ? name[1] : result.name;

        result.config = config.GetConfig(result.castingName);

        result.prefab = GetPrefabCharacter(result.castingName);

        return result;
    }

    private GameObject GetPrefabCharacter(string characterName)
    {
        string prefabPath = FormatCharacterPath(characterPrefabPath, characterName);
        return Resources.Load<GameObject>(prefabPath);
    }

    public string FormatCharacterPath(string paht, string characterName) => paht.Replace(CHARACTER_NAME_ID, characterName);

    private Character CreatCharacterFromInfo(CHARACTER_INF info)
    {
        CharacterConfigData config = info.config;

        if (config.type == Character.CharacterType.Text)
            return new Character_Text(info.name, config);
        if (config.type == Character.CharacterType.Sprite)
            return new Character_Sprite(info.name, config, info.prefab);

        return null;
    }

    private class CHARACTER_INF
    {
        public string name = "";
        public string castingName = "";

        public CharacterConfigData config = null;

        public GameObject prefab = null;
    }
}
