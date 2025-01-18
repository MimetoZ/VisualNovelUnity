using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using System;

public class TestingDialogueFiles : MonoBehaviour
{
    [SerializeField] private TextAsset fileToRead = null;
    // Start is called before the first frame update
    void Start()
    {
        StartConversation();
    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset(fileToRead);

        DialogueSystem.instance.Say(lines);
    }
}
