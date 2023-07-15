using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    [SerializeField] private BoyListener listener;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (listener.wirePuzzleCompleted)
        {
            animator.SetBool("Happy", true);
        }
    }
}
