using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanTutorialChecker : MonoBehaviour
{
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    private bool isFrom;
    private bool isTo;

    private void Awake()
    {
        isFrom = false;
        isTo = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);
            if (hit != null)
            {
                if (hit.transform.Equals(from))
                {
                    isFrom = true;

                }
                else
                {
                    isFrom = false;
                }
            } 
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isFrom)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);
                if (hits != null && hits.Length != 0)
                {
                    bool containsTo = false;
                    foreach (Collider2D collider in hits)
                    {
                        if (collider.transform.Equals(to))
                        {
                            containsTo = true;
                        }
                    }
                    if (containsTo)
                    {
                        isTo = true;
                    }
                    else
                    {
                        isFrom = false;
                        isTo = false;
                    }
                }
            }
        }
        if (isFrom && isTo)
        {
            GoToNextStep();
        }
    }

    private void GoToNextStep()
    {
        WateringCansPuzzleTracker.Instance.NextTutorialStep();
    }
}
