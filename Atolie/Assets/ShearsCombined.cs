using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShearsCombined : MonoBehaviour
{
    [SerializeField] private Transform fish;
    [SerializeField] private Transform shears;
    [SerializeField] private Animator signalAnimator;
    [SerializeField] private Text distanceText;
    private float distanceToShears;

    private void Start()
    {
        distanceToShears = CalculateDistanceToShears();
        UpdateText();
        UpdateAnimation();
    }

    private void Update()
    {
        distanceToShears = CalculateDistanceToShears();
        UpdateText();
        UpdateAnimation();
    }

    private float CalculateDistanceToShears()
    {
        float deltaX = fish.position.x - shears.position.x;
        float deltaY = fish.position.y - shears.position.y;
        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
        return distance;
    }

    private void UpdateText()
    {
        distanceText.text = distanceToShears.ToString("0");
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
        }
        else
        {
            signalAnimator.SetInteger("Level", 4);
        }
    }
}
