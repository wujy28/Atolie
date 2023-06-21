using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    //This is where we put information we want the scriptable object to hold, for now i only put image

    [Header("Only gameplay")]

    [Header("Only UI")]

    [Header("Both")]
    public Sprite image;

    [Header("Associated Interactable Name (Optional)")]
    public string interactableName;
}
