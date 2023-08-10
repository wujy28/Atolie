using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonogramGridSquare : MonoBehaviour
{
    public Image activeImage;
    public Image normalImage;
    public Image crossImage;
    public Image qnMarkImage;

    public bool isColored = false;

    public int SquareIndex { get; set; }

    private void Awake()
    {
        Deselect();
    }

    //When square is selected, change its colour and set its bool
    public void Select()
    {
        activeImage.gameObject.SetActive(true);
        crossImage.gameObject.SetActive(false);
        qnMarkImage.gameObject.SetActive(false);
        isColored = true;
    }

    //When square is unselected, change its colour back and set its bool
    public void Deselect()
    {
        activeImage.gameObject.SetActive(false);
        crossImage.gameObject.SetActive(false);
        qnMarkImage.gameObject.SetActive(false);
        isColored = false;
    }

    //Cross out square
    public void CrossOut()
    {
        activeImage.gameObject.SetActive(false);
        crossImage.gameObject.SetActive(true);
        qnMarkImage.gameObject.SetActive(false);
        isColored = false;
    }

    //Set square with a question mark
    public void QnMark()
    {
        activeImage.gameObject.SetActive(false);
        crossImage.gameObject.SetActive(false);
        qnMarkImage.gameObject.SetActive(true);
        isColored = false;
    }

    //Disables clicking for square
    public void DisableClick()
    {
        Destroy(gameObject.GetComponent<Button>());
    }

    //Function to change state of square when clicked
    public void ClickSquare()
    {
        if (!isColored && !activeImage.gameObject.activeSelf && !qnMarkImage.gameObject.activeSelf)
        {
            Select();

        } else if (isColored && activeImage.gameObject.activeSelf && !qnMarkImage.gameObject.activeSelf)
        {
            QnMark();

        } else if (!isColored && !activeImage.gameObject.activeSelf && qnMarkImage.gameObject.activeSelf)
        {
            Deselect();
        }
    }
}
