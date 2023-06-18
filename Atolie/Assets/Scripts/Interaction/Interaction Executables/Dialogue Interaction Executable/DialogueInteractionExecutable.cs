using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Interaction Executable", menuName = "Dialogue Interaction Executable")]
public class DialogueInteractionExecutable : InteractionExecutable
{
    public Dialogue dialogue;

    public DialogueInteractionExecutable(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }

    public override void execute()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}