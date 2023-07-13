using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHouse : MonoBehaviour
{
    [SerializeField] private MushroomHouseListener listener;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ElderTruffle;

    private void Start()
    {
        if (listener.mushroomHouseUnlocked)
        {
            animator.SetBool("Open", true);
        }
        UnlockMushroomHouseRunnable.OnMushroomHouseUnlocked += UnlockMushroomHouseRunnable_OnMushroomHouseUnlocked;
    }

    private void UnlockMushroomHouseRunnable_OnMushroomHouseUnlocked()
    {
        animator.SetBool("Open", true);
        ElderTruffle.SetActive(true);
    }

    private void OnDisable()
    {
        UnlockMushroomHouseRunnable.OnMushroomHouseUnlocked -= UnlockMushroomHouseRunnable_OnMushroomHouseUnlocked;
    }
}
