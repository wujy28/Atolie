using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
