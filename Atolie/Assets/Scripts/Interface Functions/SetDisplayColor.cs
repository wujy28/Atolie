using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This controls the display of the current selected color of the Paint Bucket.
/// 1 is purple, 2 is pink, 3 is blue.
/// </summary>
public class SetDisplayColor : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        changeCurrentColor(0);
    }

    public void changeCurrentColor(int color)
    {
        animator.SetInteger("Color", color);
    }
}
