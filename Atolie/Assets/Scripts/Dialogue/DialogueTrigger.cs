using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //Function to trigger the start of a dialogue
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    //Function to set instance of dialogue
    public void setDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }
}
