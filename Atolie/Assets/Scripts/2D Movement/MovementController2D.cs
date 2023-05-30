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

    // Start is called before the first frame update
    void Start()
    {
        followSpot = transform.position;
        onUpperLevel = false;
        // initial y coordinate set to the lower level.
        // (should probably assign the value as a constant for clarity)
        yPos = -3.5f;
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the position of the cursor in the world.
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Print the mouse position x and y coordinates
        Debug.Log(mousePosition.x + ", " + mousePosition.y);
        // If Left Click
        if (Input.GetMouseButton(0))
        {
            // Update target position
            followSpot = new Vector2(mousePosition.x, yPos);
            updateAnimation(followSpot.x);
        }
        // Uses Vector2 MoveTowards method to find and follow a path from current position to target position.
        transform.position = Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed);
        // Print the current position of the player
        Debug.Log(transform.position);
    }

    /// <summary>
    /// Transports the player to the upper level. Tied to the Up Button in CyberPunk City (under TempNavigation).
    /// </summary>
    public void goToUpperLevel()
    {
        if (!onUpperLevel)
        {
            // Changes target position to the landing of the staircase
            followSpot = new Vector2(-6.67f, -0.134f);
            yPos = -0.134f;
            onUpperLevel = true;
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
            followSpot = new Vector2(-3.3f, -3.5f);
            yPos = -3.5f;
            onUpperLevel = false;
        }
    }

    /// <summary>
    /// Attempts to prevent player from moving when hitting the boundary of the world,
    /// upon collision with the Boundary collider.
    /// Unfortunately the boundary does not actually stop the player since the
    /// player is a kinematic rigidbody and boundary is a trigger :(
    /// </summary>
    /// <param name="collision"></param> the collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boundary")
        {
            // Maybe this line itself does not work
            // Rapidly clicking outwide the world when player is against the boundary
            // will still cause the player to move outside, maybe cuz followSpot
            // is still updating as mouse position and player moves towards it for a
            // split moment before stopping again cuz of this method. (idk)
            followSpot = transform.position;
        }
    }

    /// <summary>
    /// Tells the player to stop moving.
    /// </summary>
    public void stopMoving()
    {
        followSpot = transform.position;
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
