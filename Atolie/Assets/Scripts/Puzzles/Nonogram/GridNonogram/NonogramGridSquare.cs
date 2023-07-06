using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonogramGridSquare : MonoBehaviour
{
    public Image activeImage;
    public Image normalImage;

    public bool isColored;

    //public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    //public bool SquareOccupied { get; set; }

    private void Awake()
    {
        Deselect();
    }

    void Start()
    {
        isColored = false;
    }

    //When square is selected, change its colour and set its bool
    public void Select()
    {
        activeImage.gameObject.SetActive(true);
        isColored = true;
    }

    //When square is unselected, change its colour back and set its bool
    public void Deselect()
    {
        activeImage.gameObject.SetActive(false);
        isColored = false;
    }

    public void ClickSquare()
    {
        if (isColored == false)
        {
            Select();
        } else
        {
            Deselect();
        }
    }
}
