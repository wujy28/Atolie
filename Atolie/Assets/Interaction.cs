using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
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

    public void tagObjectToPlayer()
    {
        player.GetComponent<InteractionController>().setCurrentTarget(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && player.GetComponent<InteractionController>().getCurrentTarget().Equals(gameObject))
        {
            Debug.Log("Interact with" + gameObject.name);
            player.GetComponent<InteractionController>().reachedCurrentTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exitInteraction();
        }
    }

    public void enterInteraction()
    {
        transform.Find("Dummy Interact").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void exitInteraction()
    {
        transform.Find("Dummy Interact").GetComponent<SpriteRenderer>().enabled = false;
    }
} 
