using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Encapsulates the coloring in behavior of interactables.
/// </summary>
public class Coloring : MonoBehaviour
{
    /// <summary>
    /// Whether the object is currently colored in.
    /// </summary>
    public bool coloredIn;

    /// <summary>
    /// The correct color for this object.
    /// </summary>
    public string correctColor;

    public static event Action<Transform> OnColoredInEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribes to the Paint Bucket's information channel
        //PaintBucketMode paintBucketMode = GameObject.Find("PaintBucket").GetComponent<PaintBucketMode>();
        ColorManager paintBucketMode = GameObject.Find("ColorManager").GetComponent<ColorManager>();
        // Add the behavior that is affected by the coloring mode on event??
        // Here, it is to glow/flash when correct color is selected during Paint Bucket Mode
        paintBucketMode.ColoringModeOnEvent += flash;
        if (!coloredIn)
        {
            // If not colored in, disable Interaction script and trigger zone.
            gameObject.GetComponent<InteractionTrigger>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Attempts to the object in with the given color.
    /// </summary>
    /// <param name="color"></param> The color of the Paint Bucket when the interactable is clicked.
    public void colorIn(string color)
    {
        if (!coloredIn && color == correctColor)
        {
            coloredIn = true;
            // Changes to colored in sprite animation
            GetComponent<Animator>().SetBool("IsColoredIn", true);
            // Enables interaction and trigger zone
            gameObject.GetComponent<InteractionTrigger>().enabled = true;
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            // Unsubscribes from Paint Bucket's information channel
            PaintBucketMode paintBucketMode = GameObject.Find("PaintBucket").GetComponent<PaintBucketMode>();
            paintBucketMode.ColoringModeOnEvent -= flash;

            OnColoredInEvent?.Invoke(transform);
        }
    }

    /// <summary>
    /// Glows/flashes when correct color is selected during Paint Bucket Mode
    /// </summary>
    /// <param name="color"></param> Current selected color of Paint Bucket
    /// <param name="on"></param> Whether Paint Bucket Mode is on
    private void flash(string color, bool on)
    {
        if (color == correctColor)
        {
            // This Coloring Mode On boolean is the condition that controls
            // whether glowing animation is played.
            GetComponent<Animator>().SetBool("ColoringModeOn", on);
        } else
        {
            GetComponent<Animator>().SetBool("ColoringModeOn", false);
        }
    }
}
