using System;
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

    private void OnEnable()
    {
        try
        {
            ColorManager.Color currentColor = ColorManager.instance.selectedColor;
            changeCurrentColor((int)currentColor);
        }
        catch (NullReferenceException e)
        {
            ColorManager.Color currentColor = FindObjectOfType<ColorManager>().selectedColor;
            changeCurrentColor((int)currentColor);
        }
    }

    public void changeCurrentColor(int color)
    {
        animator.SetInteger("Color", color);
    }
}
