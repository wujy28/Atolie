using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coloring : MonoBehaviour
{

    public bool coloredIn;
    public string correctColor;

    // Start is called before the first frame update
    void Start()
    {
        PaintBucketMode paintBucketMode = GameObject.Find("PaintBucket").GetComponent<PaintBucketMode>();
        paintBucketMode.ColoringModeOnEvent += flash;
        if (!coloredIn)
        {
            gameObject.GetComponent<Interaction>().enabled = false;
        }
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
            gameObject.GetComponent<Interaction>().enabled = true;
            PaintBucketMode paintBucketMode = GameObject.Find("PaintBucket").GetComponent<PaintBucketMode>();
            paintBucketMode.ColoringModeOnEvent -= flash;
        }
    }

    private void flash(string color, bool on)
    {
        if (color == correctColor)
        {
            GetComponent<Animator>().SetBool("ColoringModeOn", on);
        } else
        {
            GetComponent<Animator>().SetBool("ColoringModeOn", false);
        }
    }
}
