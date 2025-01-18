using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField]private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;

        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueSystem instance { get; private set; }

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        public bool isRunningConversation => conversationManager.isRunning;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialized();
            }
            else
                DestroyImmediate(gameObject);


        }

        bool _initialized = false;
        private void Initialized()
        {
            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke();
        }

        public void ApplySpeakerDataDialogueContainer(string speakerName)
        {
            Character character = CharacterManager.instance.GetCharacter(speakerName);
            CharacterConfigData config = character != null ? character.config : CharacterManager.instance.GetCharacterConfig(speakerName);


            ApplySpeakerDataDialogueContainer(config);
        }

        public void ApplySpeakerDataDialogueContainer(CharacterConfigData config)
        {
            dialogueContainer.SetDialogueColore(config.dialogueColor);
            dialogueContainer.SetDialogueFont(config.dialogueFont);
            dialogueContainer.nameContainer.SetNameeColore(config.nameColor);
            dialogueContainer.nameContainer.SetNameFont(config.nameFont);
        }

        public void ShawSpeakerName(string speakerName = "")
        {
            if (speakerName.ToLower() != "narrator")
                dialogueContainer.nameContainer.Show(speakerName);
            else
                HideSpeakerName();
        }
        public void HideSpeakerName() => dialogueContainer .nameContainer.Hide();

        public Coroutine Say(string speaker, string dialogue)
        {
            List<string> conversatoin = new List<string>() { $"{speaker} \"{dialogue}\""};
            return Say(conversatoin);
        }

        public Coroutine Say(List<string> conversaition)
        {
            return conversationManager.StartConversation(conversaition);
        }
    }
}