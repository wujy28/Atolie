using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Encapsulates Paint Bucket Mode that is enabled when the Paint Bucket Button is clicked.
/// This is the coloring in feature for interactables in the scene
/// as you progress through the game.
/// </summary>
public class PaintBucketMode : MonoBehaviour
{
    /// <summary>
    /// Whether Paint Bucket Mode is on
    /// </summary>
    public bool paintBucketModeOn;

    /// <summary>
    /// The current selected color for the Paint Bucket.
    /// </summary>
    public string currentColor;

    /// <summary>
    /// Whether player movement is currently enabled.
    /// </summary>
    private bool playerMovementEnabled;

    /// <summary>
    /// The animator associated with the Paint Bucket Button.
    /// </summary>
    public Animator buttonAnimator;

    /// <summary>
    /// The interaction controller of the player.
    /// </summary>
    private InteractionController interactionController;

    /// <summary>
    /// The movement controller 2D of the player.
    /// </summary>
    private MovementController2D movementController2D;

    /// <summary>
    /// The movement controller 2.5D of the player.
    /// </summary>
    private MovementController movementController;

    /// <summary>
    /// The player.
    /// </summary>
    private GameObject player;

    /* This creates an 'information publisher' that tells subscribers
     * when Paint Bucket Mode is on and the current selected color of the
     * Paint Bucket. Subscribers here are the interactables.
     * Tbh I still don't fully understand how to utilise this Event system thingy but
     * these videos helped: 
     * https://www.youtube.com/watch?v=OuZrhykVytg&t=435s
     * https://www.youtube.com/watch?v=gx0Lt4tCDE0&t=288s
     * 
     * It'll definitely be good if we learn to use this as this will be
     * very handy for a lot of the features we're gonna build I think
     */
    // Create a new coloring mode on event
    public event PaintBucketDelegate ColoringModeOnEvent;
    // Creates a delegate (i dont exactly know what a delegate is)
    public delegate void PaintBucketDelegate(string color, bool on);

    void Start()
    {
        paintBucketModeOn = false;
        currentColor = "noColor";
        playerMovementEnabled = true;
        player = GameObject.FindGameObjectWithTag("Player");
        interactionController = player.GetComponent<InteractionController>();
        // Finds movement controller 2D or 2.5D depending on which one is present
        if (player.TryGetComponent<MovementController2D>(out MovementController2D controller))
        {
            movementController2D = controller;
        } else
        {
            movementController = player.GetComponent<MovementController>();
        }
    }

    // Tbh I'm pretty sure there's a better way to do this than to check every
    // frame whether paint bucket mode is on lol (maybe the event system thingy)
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
            // If Left Click
            if (Input.GetMouseButtonDown(0))
            {
                // Create a ray at the position of the mouse cursor
                // Get the topmost object (I think) that the ray hit (maximum depth of 10)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Collectible"))
                    {
                        Debug.Log("Hit 2D collider: " + hit.collider.name);
                        // Tell the interactable to color itself in with the current selected color
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

    /// <summary>
    /// Disables the movement controller of the player.
    /// </summary>
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

    /// <summary>
    /// Enables the movement controller of the player.
    /// </summary>
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

    /// <summary>
    /// Toggles Paint Bucket Mode.
    /// </summary>
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
        // Notifies subscribers of the state of Paint Bucket Mode and current selected color
        // when this state changes
        ColoringModeOnEvent?.Invoke(currentColor, paintBucketModeOn);
    }

    /// <summary>
    /// Changes the current selected color of the Paint Bucket.
    /// </summary>
    /// <param name="color"></param> Color to change to
    public void changeColor(string color)
    {
        currentColor = color;
        // Notifies/updates subscribers of the change in current selected color
        ColoringModeOnEvent?.Invoke(currentColor, paintBucketModeOn);
    }
}
