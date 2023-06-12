using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Encapsulates the interaction of an interactable in the game.
/// Right now interactions are just dummy ones, and the system
/// will probably be changed later.
/// </summary>
public class NPCTrigger : InteractionTrigger
{
    /*
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Tags interactable to the player.
    /// </summary>
    public void tagObjectToPlayer()
    {
        // Sets this interactable as the player's current target interactable
        player.GetComponent<PlayerInteraction>().setCurrentTarget(transform);
    }

    /// <summary>
    /// When player enters the trigger zone of the interactable.
    /// </summary>
    /// <param name="collision"></param> The collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<PlayerInteraction>().getCurrentTarget().Equals(gameObject))
        {
            Debug.Log("Interact with " + gameObject.name);
            // Tells player that current target interactable has been reached.
            player.GetComponent<PlayerInteraction>().reachedCurrentTarget();
        }
    }

    /// <summary>
    /// Whent player leaves the trigger zone of the interactable.
    /// </summary>
    /// <param name="collision"></param> The collision
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exitInteraction();
        }
    }

    /// <summary>
    /// Enter interaction.
    /// </summary>
    public void enterInteraction()
    {
        // Display dummy interaction green box.
        transform.Find("Dummy Interact").GetComponent<SpriteRenderer>().enabled = true;
    }

    /// <summary>
    /// Exit interaction.
    /// </summary>
    public void exitInteraction()
    {
        // Hide dummy interaction green box.
        transform.Find("Dummy Interact").GetComponent<SpriteRenderer>().enabled = false;
    }
    */

    override public void OnPointerEnter(PointerEventData eventData)
    {
        getCursorController().setNPCCursor();
    }
} 
