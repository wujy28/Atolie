using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleLevelSelectManager : MonoBehaviour
{
    // Stage 1
    [SerializeField] private GameObject level_1_button;
    [SerializeField] private GameObject level_1_unlocked;
    [SerializeField] private GameObject level_1_locked;
    [SerializeField] private GameObject level_1_completed;

    // Stage 2
    [SerializeField] private GameObject level_2_button;
    [SerializeField] private GameObject level_2_unlocked;
    [SerializeField] private GameObject level_2_locked;
    [SerializeField] private GameObject level_2_completed;

    // Stage 3
    [SerializeField] private GameObject level_3_button;
    [SerializeField] private GameObject level_3_unlocked;
    [SerializeField] private GameObject level_3_locked;
    [SerializeField] private GameObject level_3_completed;

    // Stage 4
    [SerializeField] private GameObject level_4_button;
    [SerializeField] private GameObject level_4_unlocked;
    [SerializeField] private GameObject level_4_locked;
    [SerializeField] private GameObject level_4_completed;

    private void Awake()
    {
        level_1_locked.SetActive(true);
        level_2_locked.SetActive(true);
        level_3_locked.SetActive(true);
        level_4_locked.SetActive(true);
    }

    public void UnlockLevel(int level)
    {
        switch (level)
        {
            case 1:
                level_1_unlocked.SetActive(true);
                level_1_button.SetActive(true);
                level_1_locked.SetActive(false);
                level_1_completed.SetActive(false);
                break;
            case 2:
                level_2_unlocked.SetActive(true);
                level_2_button.SetActive(true);
                level_2_locked.SetActive(false);
                level_2_completed.SetActive(false);
                break;
            case 3:
                level_3_unlocked.SetActive(true);
                level_3_button.SetActive(true);
                level_3_locked.SetActive(false);
                level_3_completed.SetActive(false);
                break;
            case 4:
                level_4_unlocked.SetActive(true);
                level_4_button.SetActive(true);
                level_4_locked.SetActive(false);
                level_4_completed.SetActive(false);
                break;
        }
    }

    public void CompleteLevel(int level)
    {
        switch (level)
        {
            case 1:
                level_1_unlocked.SetActive(false);
                level_1_button.SetActive(false);
                level_1_locked.SetActive(false);
                level_1_completed.SetActive(true);
                break;
            case 2:
                level_2_unlocked.SetActive(false);
                level_2_button.SetActive(false);
                level_2_locked.SetActive(false);
                level_2_completed.SetActive(true);
                break;
            case 3:
                level_3_unlocked.SetActive(false);
                level_3_button.SetActive(false);
                level_3_locked.SetActive(false);
                level_3_completed.SetActive(true);
                break;
            case 4:
                level_4_unlocked.SetActive(false);
                level_4_button.SetActive(false);
                level_4_locked.SetActive(false);
                level_4_completed.SetActive(true);
                break;
        }
    }
}
