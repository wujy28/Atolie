using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBedsInteraction : MonoBehaviour
{
    public void interact()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        //InteractionManager.Instance.exitInteraction();
    }

    public void setTarget()
    {
        InteractionManager.Instance.setCurrentTarget(transform);
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
