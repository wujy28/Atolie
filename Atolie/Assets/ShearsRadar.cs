using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShearsRadar : MonoBehaviour
{
    [SerializeField] private Transform fish;
    [SerializeField] private Transform shears;
    [SerializeField] private Animator signalAnimator;
    [SerializeField] private GameObject tutorial;
    private float distanceToShears;

    private void Start()
    {
        PondMazeTracker.PuzzleStart += PondMazeTracker_PuzzleStart;
        tutorial.SetActive(false);
        distanceToShears = CalculateDistanceToShears();
        UpdateAnimation();
    }

    private void OnDestroy()
    {
        PondMazeTracker.PuzzleStart -= PondMazeTracker_PuzzleStart;
    }

    private void PondMazeTracker_PuzzleStart()
    {
        tutorial.SetActive(true);
        WaitForTutorialToClose();
    }

    private void WaitForTutorialToClose()
    {
        Invoke("CloseTutorial", 15);
    }

    private void CloseTutorial()
    {
        tutorial.SetActive(false);
    }

    private void Update()
    {
        distanceToShears = CalculateDistanceToShears();
        UpdateAnimation();
    }

    private float CalculateDistanceToShears()
    {
        float deltaX = fish.position.x - shears.position.x;
        float deltaY = fish.position.y - shears.position.y;
        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
        return distance;
    }

    private void UpdateAnimation()
    {
        if (distanceToShears > 50)
        {
            signalAnimator.SetInteger("Level", 0);
        }
        else if (distanceToShears > 30)
        {
            signalAnimator.SetInteger("Level", 1);
        }
        else if (distanceToShears > 15)
        {
            signalAnimator.SetInteger("Level", 2);
        }
        else if (distanceToShears > 5)
        {
            signalAnimator.SetInteger("Level", 3);
        } else
        {
            signalAnimator.SetInteger("Level", 4);
        }

    }
}
