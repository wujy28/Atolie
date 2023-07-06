using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image hoverImage;
    public Image activeImage;
    public Image normalImage;
    //public List<Sprite> normalImages;

    //public List<Image> activeImageList = new List<Image>();
    //public int indexOfActiveImage = 0;

    public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Selected = false;
        SquareOccupied = false;
    }

    //temp
    /*public bool CanWeUseThisSquare()
    {
        return hoverImage.gameObject.activeSelf;
    }*/

    public void PlaceShapeOnBoard()
    {
        ActivateSquare();
    }

    public void ActivateSquare()
    {
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        //activeImageList[indexOfActiveImage].gameObject.SetActive(true);
        Selected = true;
        SquareOccupied = true;

        //test
        
        /*if (indexOfActive >= 11)
        {
            indexOfActive = 0;
        }

        hoverImage.gameObject.SetActive(false);
        activeImageList[indexOfActive].gameObject.SetActive(true);
        indexOfActive++;
        Selected = true;
        SquareOccupied = true;*/
    }

    public void Deactivate()
    {
        activeImage.gameObject.SetActive(false);
        /*for (int i = 0; i < activeImageList.Count; i++)
        {
            activeImageList[i].gameObject.SetActive(false);
        }*/
    }

    public void ClearOccupied()
    {
        Selected = false;
        SquareOccupied = false;
    }

    /*public void SetImage(bool setFirstImage)
    {
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SquareOccupied == false)
        {
            Selected = true;
            hoverImage.gameObject.SetActive(true);
        } /*else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Selected = true;

        if (SquareOccupied == false)
        {
            hoverImage.gameObject.SetActive(true);
        } /*else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (SquareOccupied == false)
        {
            Selected = false;
            hoverImage.gameObject.SetActive(false);
        } /*else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().UnSetOccupied();
        }*/
    }    
}
