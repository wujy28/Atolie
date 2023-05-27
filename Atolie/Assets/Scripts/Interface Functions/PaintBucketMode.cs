using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketMode : MonoBehaviour
{
    public bool paintBucketModeOn;
    public string currentColor;
    private bool playerMovementEnabled;
    public Animator buttonAnimator;
    private InteractionController interactionController;
    private MovementController2D movementController2D;
    private MovementController movementController;
    private GameObject player;

    public event PaintBucketDelegate ColoringModeOnEvent;
    public delegate void PaintBucketDelegate(string color, bool on);

    void Start()
    {
        paintBucketModeOn = false;
        currentColor = "noColor";
        playerMovementEnabled = true;
        player = GameObject.FindGameObjectWithTag("Player");
        interactionController = player.GetComponent<InteractionController>();
        if (player.TryGetComponent<MovementController2D>(out MovementController2D controller))
        {
            movementController2D = controller;
        } else
        {
            movementController = player.GetComponent<MovementController>();
        }
    }

    void Update()
    {
        if (paintBucketModeOn)
        {
            if (playerMovementEnabled)
            {
                disablePlayerMovement();
                interactionController.enabled = false;
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
                        hit.collider.GetComponent<Coloring>().colorIn(currentColor);
                    }
                }
            }
        }
        if (!paintBucketModeOn)
        {
            if (!playerMovementEnabled)
            {
                enablePlayerMovement();
                interactionController.enabled = true;
                playerMovementEnabled = true;
            }
        }
        
    }

    private void disablePlayerMovement()
    {
        if (movementController != null)
        {
            movementController.enabled = false;
        }
        if (movementController2D != null)
        {
            movementController2D.enabled = false;
        }
    }

    private void enablePlayerMovement()
    {
        if (movementController != null)
        {
            movementController.enabled = true;
        }
        if (movementController2D != null)
        {
            movementController2D.enabled = true;
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
        ColoringModeOnEvent?.Invoke(currentColor, paintBucketModeOn);
    }

    public void changeColor(string color)
    {
        currentColor = color;
        ColoringModeOnEvent?.Invoke(currentColor, paintBucketModeOn);
    }
}
