using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingPuzzleDifficultyManager : MonoBehaviour
{
    [SerializeField] private GameObject easyMode;
    [SerializeField] private GameObject hardMode;
    [SerializeField] private Button switchToEasyMode;
    [SerializeField] private Button switchToHardMode;
    [SerializeField] private GameObject easyModeTimer;
    [SerializeField] private Text easyModeCountdown;
    [SerializeField] private GameObject easyModeText;

    [SerializeField] private Difficulty currentDifficulty;

    public static event Action OnReset;

    private float currentTime = 0f;
    private float startingTime = 120f;

    private void Awake()
    {
        SwitchDifficulty(Difficulty.Hard);
        easyModeTimer.SetActive(true);
        easyModeText.SetActive(false);
        switchToEasyMode.interactable = false;
        switchToHardMode.interactable = true;
    }

    public void StartTimer()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            easyModeCountdown.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                easyModeTimer.SetActive(false);
                easyModeText.SetActive(true);
                switchToEasyMode.interactable = true;
            }
        }
    }

    private void SwitchDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                switchToEasyMode.gameObject.SetActive(false);
                hardMode.SetActive(false);
                easyMode.SetActive(true);
                switchToHardMode.gameObject.SetActive(true);
                break;
            case Difficulty.Hard:
                switchToHardMode.gameObject.SetActive(false);
                easyMode.SetActive(false);
                hardMode.SetActive(true);
                switchToEasyMode.gameObject.SetActive(true);
                break;
        }
    }

    public void SwitchToEasyMode()
    {
        SwitchDifficulty(Difficulty.Easy);
    }

    public void SwitchToHardMode()
    {
        SwitchDifficulty(Difficulty.Hard);
    }

    public void ResetActiveBlocks()
    {
        OnReset?.Invoke();
    }

    private enum Difficulty
    {
        Easy,
        Hard
    }
}
