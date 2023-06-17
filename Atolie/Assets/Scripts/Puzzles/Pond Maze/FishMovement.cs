using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private bool faceLeft;
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        faceLeft = true;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateAnimation(movement.x);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void UpdateAnimation(float xVelocity)
    {
        if (xVelocity > 0 && faceLeft)
        {
            sr.flipX = true;
            faceLeft = false;
        }
        else if (xVelocity < 0 && !faceLeft)
        {
            sr.flipX = false;
            faceLeft = true;
        }
    }

}
