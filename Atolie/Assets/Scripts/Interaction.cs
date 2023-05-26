using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public bool coloredIn;
    public string correctColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void colorIn(string color)
    {
        if (!coloredIn && color == correctColor)
        {
            coloredIn = true;
            GetComponent<Animator>().SetBool("IsColoredIn", true);
        }
    }
}
