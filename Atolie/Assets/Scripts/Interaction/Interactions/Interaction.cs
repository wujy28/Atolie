using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    public InteractionExecutable[] executables;

    public Interaction(InteractionExecutable[] executables)
    {
        this.executables = executables;
    }
}
