using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HeneGames.DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        #region Singleton

        public static DialogueUI instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        private DialogueManager currentDialogueManager;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private GameObject dialogueWindow;
        [SerializeField] private GameObject interactionUI;

        [Header("Next sentence input")]
        public KeyCode actionInput = KeyCode.Space;

        private void Start()
        {
            //Hide dialogue and interaction UI at start
            dialogueWindow.SetActive(false);
            interactionUI.SetActive(false);
        }

        private void Update()
        {
            //Continue only if we have dialogue
            if (currentDialogueManager == null)
                return;

            //Next dialogue input
            if (Input.GetKeyDown(actionInput))
            {
                //Tell the current dialogue manager to display the next sentence. This function also gives information if we are at the last sentence
                currentDialogueManager.NextSentence(out bool lastSentence);

                //If last sentence remove current dialogue manager
                if(lastSentence)
                {
                    currentDialogueManager = null;
                }
            }
        }

        public void StartDialogue(DialogueManager _dialogueManager)
        {
            //Store dialogue manager
            currentDialogueManager = _dialogueManager;

            //Start displaying dialogue
            currentDialogueManager.StartDialogue();
        }

        public void ShowSentence(DialogueCharacter _dialogueCharacter, string _message)
        {
            dialogueWindow.SetActive(true);

            nameText.text = _dialogueCharacter.characterName;
            messageText.text = _message;
        }

        public void ClearText()
        {
            dialogueWindow.SetActive(false);
        }

        public void ShowInteractionUI(bool _value)
        {
            interactionUI.SetActive(_value);
        }
    }
}