using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Image image;

    //Destroys current instruction screen to reveal next set of instructions
    public void ContinueInstruction()
    {
        Destroy(image.gameObject);
    }
}
