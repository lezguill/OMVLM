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
            GetComponent<Animator>().SetBool("isDialoguing", true);
            GetComponent<Animator>().SetTrigger("DialogueAction");
        }
        public void EndDialogue()
        {
            GetComponent<Animator>().SetBool("isDialoguing", false);
        }
        public void DialogueAction()
        {
            GetComponent<Animator>().SetTrigger("DialogueAction");
        }
    }
}