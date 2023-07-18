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

    //public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    //public bool SquareOccupied { get; set; }

    private void Awake()
    {
        Deselect();
    }

    void Start()
    {
        //isColored = false;
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

    public void CrossOut()
    {
        activeImage.gameObject.SetActive(false);
        crossImage.gameObject.SetActive(true);
        qnMarkImage.gameObject.SetActive(false);
        isColored = false;
    }

    public void QnMark()
    {
        activeImage.gameObject.SetActive(false);
        crossImage.gameObject.SetActive(false);
        qnMarkImage.gameObject.SetActive(true);
        isColored = false;
    }

    public void DisableClick()
    {
        Destroy(gameObject.GetComponent<Button>());
    }

    public void ClickSquare()
    {
        /*if (!isColored)
        {
            Select();

        } else
        {
            Deselect();
        }*/

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
