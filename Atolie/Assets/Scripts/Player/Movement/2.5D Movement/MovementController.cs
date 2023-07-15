using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Point and Click Controller for Player Movement in 2.5D.
///
/// Full navigation system utilizes NavMeshPlus
/// (GitHub Package: https://github.com/h8man/NavMeshPlus,
/// Youtube Tutorial: https://www.youtube.com/watch?v=C_DyjsPhKK8&t=877s).
/// </summary>
public class MovementController : MonoBehaviour
{
    /// <summary>
    /// Speed at which player moves.
    /// </summary>
    /*
     * Note: I think this field is actually redundant since the movement is
     * taken care of by NavMesh and not the Vector2 MoveTowards.
     */ 
    public float speed;

    /// <summary>
    /// Spot in the world that the player is directed to move to (target position).
    /// </summary>
    private Vector2 followSpot;

    /// <summary>
    /// Agent that moves in the NavMesh. The agent here is the player.
    /// </summary>
    private NavMeshAgent agent;

    /// <summary>
    /// Whether the Iro Sprite is facing right.
    /// </summary>
    private bool faceRight;

    // Start is called before the first frame update
    void Awake()
    {
        followSpot = transform.position;
        agent = GetComponent<NavMeshAgent>();
        // To prevent the player from rotating?
        transform.rotation = new Quaternion(0,0,0,0);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Gets the position of the cursor in the world.
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // If left click
        if (Input.GetMouseButtonDown(0))
        {
            // Update target position
            followSpot = new Vector2(mousePosition.x, mousePosition.y);
            InteractionManager.Instance.removeTarget();
            updateAnimation(followSpot.x);
        }
        */

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask hitLayers = LayerMask.GetMask("Game World") | LayerMask.GetMask("Clickable World Space");
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10, hitLayers);
            if (hit.collider != null)
            {
                followSpot = new Vector2(mousePosition.x, mousePosition.y);
                InteractionManager.Instance.removeTarget();
                updateAnimation(followSpot.x);
            }
        }

        // NavMesh method to direct agent from current position to target position
        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));

        // Redundant line here from when before NavMesh was used. Kept in case we want to change movement.
        // transform.position = Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed);
    }


    public void moveToFollowSpot(Vector2 target)
    {
        followSpot = target;
        InteractionManager.Instance.removeTarget();
        updateAnimation(followSpot.x);
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
        } else if (followSpotX < transform.position.x && faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            faceRight = false;
        }
    }
}
