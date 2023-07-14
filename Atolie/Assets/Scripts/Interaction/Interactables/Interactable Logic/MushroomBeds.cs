using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBeds : MonoBehaviour
{
    [SerializeField] private MushroomBedsListener listener;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (listener.watered)
        {
            animator.SetBool("Watered", true);
        }
        WaterMushroomBedsRunnable.OnMushroomBedsWatered += WaterMushroomBedsRunnable_OnMushroomBedsWatered;
    }

    private void WaterMushroomBedsRunnable_OnMushroomBedsWatered()
    {
        animator.SetBool("Watered", true);
    }

    private void OnDisable()
    {
        WaterMushroomBedsRunnable.OnMushroomBedsWatered -= WaterMushroomBedsRunnable_OnMushroomBedsWatered;
    }
}
