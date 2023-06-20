using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Point and Click Controller for Player Movement in 2D.
///
/// Does not utilize NavMesh.
/// </summary>
public class MovementController2D : MonoBehaviour
{
    /// <summary>
    /// Spot in the world that the player is directed to move to (target position).
    /// </summary>
    private Vector2 followSpot;

    /// <summary>
    /// Whether the Iro Sprite is facing right.
    /// </summary>
    private bool faceRight;

    /// <summary>
    /// Speed at which player moves.
    /// </summary>
    public float speed;

    /// <summary>
    /// Whether the player is on the upper level.
    /// </summary>
    private bool onUpperLevel;

    /// <summary>
    /// The y coordinate of the player.
    /// </summary>
    private float yPos;

    private float minX = -6.7f;

    private float maxX = 6.7f;

    private bool switchingLevels;

    public float upperY;

    public float lowerY;

    // Start is called before the first frame update
    void Start()
    {
        followSpot = transform.position;
        onUpperLevel = false;
        // initial y coordinate set to the lower level.
        // (should probably assign the value as a constant for clarity)
        yPos = -3.5f;
        faceRight = true;
        switchingLevels = false;
    }

    private void OnDisable()
    {
        followSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y != upperY && transform.position.y != lowerY)
        {
            switchingLevels = true;
        } else
        {
            switchingLevels = false;
        }
        // Gets the position of the cursor in the world.
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Print the mouse position x and y coordinates
        // Debug.Log(mousePosition.x + ", " + mousePosition.y);
        // If Left Click
        if (Input.GetMouseButton(0) && !switchingLevels)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask hitLayers = LayerMask.GetMask("Game World") | LayerMask.GetMask("Clickable World Space");
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10, hitLayers);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (mousePosition.x > maxX)
                {
                    followSpot = new Vector2(maxX, yPos);
                }
                else if (mousePosition.x < minX)
                {
                    followSpot = new Vector2(minX, yPos);
                }
                else
                {
                    followSpot = new Vector2(mousePosition.x, yPos);
                }
                //InteractionManager.Instance.removeTarget();
                updateAnimation(followSpot.x);
            }
        }
        // Uses Vector2 MoveTowards method to find and follow a path from current position to target position.
        transform.position = Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed);
    }

    /// <summary>
    /// Transports the player to the upper level. Tied to the Up Button in CyberPunk City (under TempNavigation).
    /// </summary>
    public void goToUpperLevel()
    {
        if (!onUpperLevel)
        {
            // Changes target position to the landing of the staircase
            followSpot = new Vector2(-6.67f, upperY);
            yPos = upperY;
            onUpperLevel = true;
            switchingLevels = true;
        }
    }

    /// <summary>
    /// Transports the player to the lower level. Tied to the Down Button in CyberPunk City (under TempNavigation).
    /// </summary>
    public void goToLowerLevel()
    {
        if (onUpperLevel)
        {
            // Changes target position to the base of the staircase
            followSpot = new Vector2(-3.3f, lowerY);
            yPos = lowerY;
            onUpperLevel = false;
            switchingLevels = true;
        }
    }

    /// <summary>
    /// Updates the direction that the Iro Sprite is facing based on target position.
    /// </summary>
    /// <param name="followSpotX"></param> the x coordinate of the target position
    private void updateAnimation(float followSpotX)
    {
        if (followSpotX > transform.position.x && !faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            faceRight = true;
        }
        else if (followSpotX < transform.position.x && faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            faceRight = false;
        }
    }
}
