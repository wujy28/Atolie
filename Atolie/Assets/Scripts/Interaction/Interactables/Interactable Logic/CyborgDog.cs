using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgDog : MonoBehaviour
{
    [SerializeField] private CyborgDogListener listener;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (listener.wirePuzzleCompleted)
        {
            animator.SetBool("Fixed", true);
        }
    }
}
