using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.transform.name == "Dollar Bill")
        {
            if (collision.transform.TryGetComponent<SlidingPuzzleBlock>(out SlidingPuzzleBlock dollar))
            {
                TrashCanSlidingPuzzleTracker.Instance.UpdateStage(TrashCanSlidingPuzzleTracker.Stage.GoalReached);
                dollar.MoveDollarBillToTrigger();
            }
        }
    }
}
