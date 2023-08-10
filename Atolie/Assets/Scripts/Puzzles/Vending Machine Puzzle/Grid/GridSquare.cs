using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image hoverImage;
    public Image activeImage;
    public Image normalImage;

    public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied { get; set; }

    void Start()
    {
        Selected = false;
        SquareOccupied = false;
    }

    //Function to place shape on the grid
    public void PlaceShapeOnBoard()
    {
        ActivateSquare();
    }

    //Function to change grid square when shape has been placed on board
    public void ActivateSquare()
    {
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        Selected = true;
        SquareOccupied = true;
    }

    //Function to set the grid square back to original appearance 
    public void Deactivate()
    {
        activeImage.gameObject.SetActive(false);
    }

    //Function to clear grid square from being occupied with shape
    public void ClearOccupied()
    {
        Selected = false;
        SquareOccupied = false;
    }

    //Functions for when shape is hovering over grid squares
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SquareOccupied == false)
        {
            Selected = true;
            hoverImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Selected = true;

        if (SquareOccupied == false)
        {
            hoverImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (SquareOccupied == false)
        {
            Selected = false;
            hoverImage.gameObject.SetActive(false);
        }
    }    
}
