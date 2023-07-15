using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{
    [SerializeField] private PondListener listener;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (listener.pondTrimmed)
        {
            animator.SetBool("Trimmed", true);
        }
        TrimPondRunnable.OnPondTrimmed += TrimPondRunnable_OnPondTrimmed;
    }

    private void TrimPondRunnable_OnPondTrimmed()
    {
        animator.SetBool("Trimmed", true);
    }

    private void OnDisable()
    {
        TrimPondRunnable.OnPondTrimmed -= TrimPondRunnable_OnPondTrimmed;
    }
}
