using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketMode : MonoBehaviour
{
    public bool paintBucketModeOn;
    public string currentColor;
    private bool playerMovementEnabled;
    public Animator buttonAnimator;

    void Start()
    {
        paintBucketModeOn = false;
        currentColor = "noColor";
        playerMovementEnabled = true;
    }

    void Update()
    {
        if (paintBucketModeOn)
        {
            if (playerMovementEnabled)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PointAndClickController>().enabled = false;
                playerMovementEnabled = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Collectible"))
                    {
                        Debug.Log("Hit 2D collider: " + hit.collider.name);
                        hit.collider.GetComponent<Interaction>().colorIn(currentColor);
                    }
                }
            }
        }
        if (!paintBucketModeOn)
        {
            if (!playerMovementEnabled)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PointAndClickController>().enabled = true;
                playerMovementEnabled = true;
            }
        }
        
    }

    public void togglePaintBucketMode()
    {
        if (paintBucketModeOn)
        {
            paintBucketModeOn = false;
            buttonAnimator.SetBool("ColorModeOn", false);
        }
        else
        {
            paintBucketModeOn = true;
            buttonAnimator.SetBool("ColorModeOn", true);
        }
    }

    public void changeColor(string color)
    {
        currentColor = color;
    }
}
