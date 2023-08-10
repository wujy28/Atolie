using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                if (transform.position.y > 0.75f || transform.position.y < 0.15f) //requirement for normal hit
                {
                    Debug.Log("Normal Hit");
                    DDRManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

                } else if (transform.position.y > 0.55f || transform.position.y < 0.30f)  //requirement for good hit
                {
                    Debug.Log("Good Hit");
                    DDRManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);

                } else //else it is perfect hit
                {
                    Debug.Log("Perfect Hit"); 
                    DDRManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;

                DDRManager.instance.NoteMissed();
                Instantiate(missEffect, new Vector3(transform.position.x, 0, 0), missEffect.transform.rotation);
            }
        }
    }
}
