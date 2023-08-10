using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    //Ensure that slots are deselected at the start
    private void Awake()
    {
        Deselect();
    }

    //When an inventory slot is selected, change its colour
    public void Select()
    {
        image.color = selectedColor;
    }

    //Changes color to original color if an inventory slot is not selected
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
}
