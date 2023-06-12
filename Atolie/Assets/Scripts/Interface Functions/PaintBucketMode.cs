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
    /// The animator associated with the Paint Bucket Button.
    /// </summary>
    public Animator buttonAnimator;

    /*
    /// <summary>
    /// The interaction controller of the player.
    /// </summary>
    private PlayerInteraction interactionController;

    /// <summary>
    /// The movement controller 2D of the player.
    /// </summary>
    private MovementController2D movementController2D;

    /// <summary>
    /// The movement controller 2.5D of the player.
    /// </summary>
    private MovementController movementController;
    */

    /// <summary>
    /// The player.
    /// </summary>
    private Transform player;

    private PlayerSettings playerSettings;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSettings = player.GetComponent<PlayerSettings>();
    }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    // Tbh I'm pretty sure there's a better way to do this than to check every
    // frame whether paint bucket mode is on lol (maybe the event system thingy)
    void Update()
    {
        if (paintBucketModeOn)
        {
            // If Left Click
            if (Input.GetMouseButtonDown(0))
            {
                // Create a ray at the position of the mouse cursor
                // Get the topmost object (I think) that the ray hit (maximum depth of 10)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10, LayerMask.GetMask("Game World"));
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
    }

    /// <summary>
    /// Toggles Paint Bucket Mode.
    /// </summary>
    public void togglePaintBucketMode()
    {
        if (paintBucketModeOn)
        {
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            buttonAnimator.SetBool("ColorModeOn", false);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.PaintBucketMode);
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

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Exploration:
                paintBucketModeOn = false;
                break;
            case GameState.Interaction:
                paintBucketModeOn = false;
                break;
            case GameState.PaintBucketMode:
                paintBucketModeOn = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
