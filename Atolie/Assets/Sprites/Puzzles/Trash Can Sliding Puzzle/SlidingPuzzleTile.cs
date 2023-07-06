using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingPuzzleTile : MonoBehaviour
{
    public int tileID;
    private SlidingPuzzleBlock currentBlock;
    public Vector2 position;

    void Awake()
    {
        position = transform.position;
        currentBlock = null;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentBlock == null)
        {
            if (collision.CompareTag("Interactable"))
            {
                if (collision.TryGetComponent(out SlidingPuzzleBlock block))
                {
                    currentBlock = block;
                    block.AddToContactedTiles(this);
                    Debug.Log("Enter Tile");
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        currentBlock = null;
        if (collision.CompareTag("Interactable"))
        {
            if (collision.TryGetComponent(out SlidingPuzzleBlock block))
            {
                block.RemoveFromContactedTiles(this);
            }
        }
    }
}
