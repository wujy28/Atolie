using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Point and click controller for interactions in the game.
/// Uses Raycast to determine object underneath cursor.
/// System might be changed in the future.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    /// <summary>
    /// The current target object to interact with.
    /// </summary>
    [SerializeField] private Transform currentTarget;
    private InteractionManager interactionManager;

    [SerializeField] private float overlapCircleRadius;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = null;
        interactionManager = InteractionManager.Instance;
    }

    void Awake()
    {
        currentTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
                    // Set current target to this interactable
                    currentTarget = hit.collider.gameObject;
                    Debug.Log("Hit 2D collider: " + hit.collider.name);
                    // Only if the interaction script of the interactable is enabled.
                    // In most cases it is only enabled if it is colored in.
                    if (currentTarget.GetComponent<Interaction>().enabled)
                    {
                        // Tell the interactable to tag itself to the player
                        currentTarget.GetComponent<Interaction>().tagObjectToPlayer();
                    }
                }
            }
        }
        */

        if (currentTarget != null && !MovementController2D.switchingLevels)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, overlapCircleRadius, LayerMask.GetMask("Game World"));
            if (hits != null && hits.Length != 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.CompareTag("Interactable") || hit.CompareTag("NPC") || hit.CompareTag("Collectible"))
                    {
                        if (hit.transform == currentTarget)
                        {
                            Debug.Log("Reached " + hit.name);
                            interactionManager.enterInteraction();
                            currentTarget = null;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, overlapCircleRadius);
    }

    /*
    /// <summary>
    /// Tell interactable to enter interaction when player has reached the interactable.
    /// </summary>
    public void reachedCurrentTarget()
    {
        if (currentTarget.GetComponent<Interaction>().enabled)
        {
            currentTarget.GetComponent<Interaction>().enterInteraction();
        }
    }
    */

    /// <summary>
    /// Changes the current target to the new specified one.
    /// </summary>
    /// <param name="target"></param> The new target
    public void setCurrentTarget(Transform target)
    {
        // Tells the previous interactable to exit interaction?
        // Not sure whether this actually works properly
        currentTarget = target;
    }

    /// <summary>
    /// Returns the current target of the player.
    /// </summary>
    /// <returns></returns> The current target interactable
    public Transform getCurrentTarget()
    {
        return currentTarget;
    }
}
