using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingPuzzleBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private bool lockX;
    public int length;
    private Rigidbody2D rb;

    [SerializeField] private bool goalReached;
    private Vector2 initialPosition;

    private List<SlidingPuzzleTile> contactedTiles;

    public void Awake()
    {
        goalReached = false;
        contactedTiles = new List<SlidingPuzzleTile>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        switch (lockX)
        {
            case true:
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
                break;
            case false:
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
                break;
        }
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        SlidingPuzzleDifficultyManager.OnReset += SlidingPuzzleDifficultyManager_OnReset;
    }

    private void SlidingPuzzleDifficultyManager_OnReset()
    {
        ResetPosition();
    }

    private void OnDisable()
    {
        SlidingPuzzleDifficultyManager.OnReset -= SlidingPuzzleDifficultyManager_OnReset;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        rb.MovePosition(transform.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (lockX)
        {
            case true:
                rb.MovePosition(new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                break;
            case false:
                rb.MovePosition(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y));
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        List<SlidingPuzzleTile> tiles = CalculateDistanceToContactedTiles();
        if (tiles.Count > 0)
        {
            float distanceSum = 0;
            foreach (SlidingPuzzleTile tile in tiles)
            {
                if (lockX)
                {
                    distanceSum += tile.position.y;

                }
                else
                {
                    distanceSum += tile.position.x;
                }
            }
            float average = distanceSum / length;

            if (lockX)
            {
                rb.MovePosition(new Vector2(transform.position.x, average));
            }
            else
            {
                rb.MovePosition(new Vector2(average, transform.position.y));
            }
        }
        Invoke("ChangeRBToKinematic", 0.1f);
    }

    private void ChangeRBToKinematic()
    {
        if (!goalReached)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private List<SlidingPuzzleTile> CalculateDistanceToContactedTiles()
    {
        Dictionary<float, SlidingPuzzleTile> distances = new Dictionary<float, SlidingPuzzleTile>();
        List<float> distanceValues = new List<float>();
        foreach (SlidingPuzzleTile tile in contactedTiles)
        {
            float distance = 0;
            if (lockX)
            {
                distance = Mathf.Abs(tile.position.y - transform.position.y);
            } else
            {
                distance = Mathf.Abs(tile.position.x - transform.position.x);
            }
            distances.Add(distance, tile);
            distanceValues.Add(distance);
        }
        distanceValues.Sort();
        Debug.Log(ListToString(distanceValues));
        List<SlidingPuzzleTile> listOfTiles = new List<SlidingPuzzleTile>();
        if (length <= distanceValues.Count)
        {
            for (int i = 0; i < length; i++)
            {
                listOfTiles.Add(distances.GetValueOrDefault(distanceValues[i]));
            }
        }
        return listOfTiles;
    }

    private string ListToString<T>(List<T> list)
    {
        string str = "";
        foreach (T value in list)
        {
            str += value.ToString();
            str += " ";
        }
        return str;
    }

    public void AddToContactedTiles(SlidingPuzzleTile tile)
    {
        if (!contactedTiles.Contains(tile))
        {
            contactedTiles.Add(tile);
        }
    }

    public void RemoveFromContactedTiles(SlidingPuzzleTile tile)
    {
        if (contactedTiles.Contains(tile))
        {
            contactedTiles.Remove(tile);
        }
    }

    public void MoveDollarBillToTrigger()
    {
        goalReached = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.AddForce(transform.up * 2, ForceMode2D.Impulse);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
