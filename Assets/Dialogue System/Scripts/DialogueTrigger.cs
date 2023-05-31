using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeneGames.DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;

        public void StartDialogue()
        {
            GetComponent<Animator>().SetBool("Dialogue", true);
            GetComponent<Animator>().SetTrigger("Dialoguing");
        }
        public void EndDialogue()
        {
            GetComponent<Animator>().SetBool("Dialogue", false);
        }
        public void SetDialoguing()
        {
            GetComponent<Animator>().SetTrigger("Dialoguing");
        }
    }
}