using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController2D : MonoBehaviour
{
    private Vector2 followSpot;
    private bool faceRight;
    public float speed;
    private bool onUpperLevel;
    private float yPos;

    // Start is called before the first frame update
    void Start()
    {
        followSpot = transform.position;
        onUpperLevel = false;
        yPos = -3.5f;
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition.x + ", " + mousePosition.y);
        if (Input.GetMouseButton(0))
        {
            followSpot = new Vector2(mousePosition.x, yPos);
            updateAnimation(followSpot.x);
        }
        transform.position = Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed);
        Debug.Log(transform.position);
    }

    public void goToUpperLevel()
    {
        if (!onUpperLevel)
        {
            followSpot = new Vector2(-6.67f, -0.134f);
            yPos = -0.134f;
            onUpperLevel = true;
        }
    }

    public void goToLowerLevel()
    {
        if (onUpperLevel)
        {
            followSpot = new Vector2(-3.3f, -3.5f);
            yPos = -3.5f;
            onUpperLevel = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boundary")
        {
            followSpot = transform.position;
        }
    }

    public void stopMoving()
    {
        followSpot = transform.position;
    }

    private void updateAnimation(float followSpotX)
    {
        if (followSpotX > transform.position.x && !faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            faceRight = true;
        }
        else if (followSpotX < transform.position.x && faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            faceRight = false;
        }
    }
}
