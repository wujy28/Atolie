using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2D : MonoBehaviour
{
    private Vector2 followSpot;
    private bool faceRight;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        followSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition.x + ", " + mousePosition.y);
        if (Input.GetMouseButton(0))
        {
            followSpot = new Vector2(mousePosition.x, transform.position.y);
            if (followSpot.y >= -1.5)
            {
                MoveToUpperLevel();
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed);
    }

    private void MoveToUpperLevel()
    {
        /*
        Vector2 stairBase = new Vector2(-2.77f, -3.823f);
        Vector2 stairTop = new Vector2(-5.94f, -0.502f);
        transform.position = Vector2.MoveTowards(transform.position, stairBase, Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(stairBase, stairTop, Time.deltaTime * speed);
        */
        transform.position = new Vector2(-5.94f, -0.502f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boundary")
        {
            followSpot = transform.position;
        }
    }
}
