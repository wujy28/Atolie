using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    //Information of the scriptable object (inventory item)

    [Header("Only gameplay")]

    [Header("Only UI")]

    [Header("Both")]
    public Sprite image;

    [Header("Associated Interactable Name (Optional)")]
    public string interactableName;
}
