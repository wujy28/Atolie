using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToShears : MonoBehaviour
{
    [SerializeField] private Transform fish;
    [SerializeField] private Transform shears;
    [SerializeField] private Text distanceText;
    private float distanceToShears;

    private void Start()
    {
        distanceToShears = CalculateDistanceToShears();
        UpdateText();
    }

    private void Update()
    {
        distanceToShears = CalculateDistanceToShears();
        UpdateText();
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
}
