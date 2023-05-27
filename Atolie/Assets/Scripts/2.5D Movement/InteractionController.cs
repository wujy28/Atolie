using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    private GameObject currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Collectible"))
                {
                    currentTarget = hit.collider.gameObject;
                    Debug.Log("Hit 2D collider: " + hit.collider.name);
                    if (currentTarget.GetComponent<Interaction>().enabled)
                    {
                        currentTarget.GetComponent<Interaction>().tagObjectToPlayer();
                    }
                }
            }
        }
    }

    public void reachedCurrentTarget()
    {
        if (currentTarget.GetComponent<Interaction>().enabled)
        {
            currentTarget.GetComponent<Interaction>().enterInteraction();
        }
    }


    public void setCurrentTarget(GameObject target)
    {
        currentTarget.GetComponent<Interaction>().exitInteraction();
        currentTarget = target;
    }

    public GameObject getCurrentTarget()
    {
        return currentTarget;
    }
}
