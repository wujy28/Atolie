using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleCompletion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Interactable"))
        {
            TrashCanSlidingPuzzleTracker.Instance.UpdateStage(TrashCanSlidingPuzzleTracker.Stage.PuzzleComplete);
        }
    }
}
