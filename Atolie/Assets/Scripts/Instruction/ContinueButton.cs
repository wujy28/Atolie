using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Image image;

    public void ContinueInstruction()
    {
        Destroy(image.gameObject);
    }
}
