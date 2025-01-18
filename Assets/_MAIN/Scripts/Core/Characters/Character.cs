using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Character
{
    public const bool ENABLE_ON_START = false;

    public string Name = "";
    public string displayName = "";
    public RectTransform root = null;
    public CharacterConfigData config;
    public Animator animator;

    protected CharacterManager manager => CharacterManager.instance;
    public DialogueSystem dialogueSystem => DialogueSystem.instance;

    //Coroutines
    protected Coroutine co_revealing, co_hiding;
    public bool isRevealing => co_revealing != null;
    public bool isHiding => co_hiding != null;

    public virtual bool isVisible => false;

    public Character(string name, CharacterConfigData config, GameObject prefab)
    {
        this.Name = name;
        displayName = name;
        this.config = config;

        if (prefab != null)
        {
            GameObject ob = Object.Instantiate(prefab, manager.characterPanel);
            ob.SetActive(true);
            ob.name = manager.FormatCharacterPath(manager.characterPrefabName, name);
            root = ob.GetComponent<RectTransform>();
            animator = root.GetComponentInChildren<Animator>();
        }
    }

    public Coroutine Say(string dialogue) => Say(new List<string> { dialogue });
    public Coroutine Say(List<string> dialogue)
    {
        dialogueSystem.ShawSpeakerName(displayName);
        UpdataTextCustomizationsOnScreen();
        return dialogueSystem.Say(dialogue);
    }

    public void SetNameFont(TMP_FontAsset font) => config.nameFont = font;
    public void SetDialcolorogueFont(TMP_FontAsset font) => config.dialogueFont = font;
    public void SetNameColor(Color color) => config.nameColor = color;
    public void SetDialogueColor(Color color) => config.dialogueColor = color;
    public void ResetConfigData() => config = CharacterManager.instance.GetCharacterConfig(Name);
    public void UpdataTextCustomizationsOnScreen() => dialogueSystem.ApplySpeakerDataDialogueContainer(config);

    public virtual Coroutine Show()
    {
        if (isRevealing)
            return co_revealing;

        if (isHiding)
            manager.StopCoroutine(co_hiding);

        co_revealing = manager.StartCoroutine(ShowingOrHiding(true));

        return co_revealing;
    }

    public virtual Coroutine Hide()
    {
        if (isHiding)
            return co_hiding;

        if (isRevealing)
            manager.StopCoroutine(co_revealing);

        co_hiding = manager.StartCoroutine(ShowingOrHiding(false));

        return co_hiding;
    }

    public virtual IEnumerator ShowingOrHiding(bool show)
    {
        yield return null;
    }

    public virtual void SetPosition(Vector2 position)
    {
        (Vector2 minAnchronTarget, Vector2 maxAnchronTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);

        root.anchorMin = minAnchronTarget;
        root.anchorMax = maxAnchronTarget;
    }

    protected (Vector2, Vector2) ConvertUITargetPositionToRelativeCharacterAnchorTargets(Vector2 position)
    {
        Vector2 padding = root.anchorMax - root.anchorMin;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchronTarget = new Vector2(maxX * position.x, maxY * position.y);
        Vector2 maxAnchronTarget = minAnchronTarget + padding;

        return (minAnchronTarget, maxAnchronTarget);
    }

    public enum CharacterType
    {
        Text,
        Sprite
    }
}
