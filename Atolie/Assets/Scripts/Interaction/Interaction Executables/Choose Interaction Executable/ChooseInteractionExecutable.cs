using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choose Interaction Executable", menuName = "Interaction Executable/Choose Interaction Executable")]
public class ChooseInteractionExecutable : InteractionExecutable
{
    [SerializeField] private List<List<Interaction>> allInteractions;

    public override void execute()
    {

    }
}
