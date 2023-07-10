using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obtain Color Executable", menuName = "Interaction Executable/Obtain Color Executable")]
public class ObtainColorExecutable : InteractionExecutable
{
    public ColorManager.Color color;

    public override void execute()
    {
        ObtainColorPopup popup = ObtainColorPopup.Instance;
        popup.updatePopup(color);
        popup.ShowPopup();
        ColorManager.instance.AddColor(color);
    }
}
