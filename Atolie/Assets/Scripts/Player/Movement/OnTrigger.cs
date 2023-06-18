using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects when player has entered trigger zone of world boundary for 2D world (city).
/// Does not actually stop player from phasing through the wall lol.
/// </summary>
public class OnTrigger : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Tells player to stop moving.
            player.GetComponent<MovementController2D>().stopMoving();
        }
    }
}
