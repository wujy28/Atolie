using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Submit Item Executable", menuName = "Interaction Executable/Submit Item Executable")]
public class SubmitItemExecutable : InteractionExecutable
{
    public Item requiredItem;

    public override void execute()
    {
        SubmitItemPopup popup = SubmitItemPopup.Instance;
        popup.updatePopup(requiredItem);
        popup.ShowPopup();
    }
}
