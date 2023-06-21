using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obtain Item Executable", menuName = "Interaction Executable/Obtain Item Executable")]
public class ObtainItemExecutable : InteractionExecutable
{
    public Item item;

    public override void execute()
    {
        ObtainItemPopup popup = ObtainItemPopup.Instance;
        popup.updatePopup(item);
        popup.ShowPopup();
        InventoryManager.instance.AddItem(item);
        InteractionManager.Instance.RemoveFromScene(item);
    }
}
