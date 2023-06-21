using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Interaction Executable", menuName = "Interaction Executable/Dialogue Interaction Executable")]
public class DialogueInteractionExecutable : InteractionExecutable
{
    public Dialogue dialogue;

    public DialogueInteractionExecutable(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }

    public override void execute()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}