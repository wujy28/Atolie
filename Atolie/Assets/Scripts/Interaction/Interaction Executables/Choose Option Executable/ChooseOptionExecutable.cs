using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Choose Option Executable", menuName = "Choose Option Executable")]
public class ChooseOptionExecutable : InteractionExecutable
{
    [TextArea(3, 10)]
    public string description;
    public string enterText;
    public string enterName;
    public string exitText;
    public string exitName;

    public override void execute()
    {
        ChooseOptionPopup popup = ChooseOptionPopup.Instance;
        popup.ShowPopup();
        popup.updatePopup(description, enterText, enterName, exitText, exitName);
    }
}

