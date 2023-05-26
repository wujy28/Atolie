using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickController : MonoBehaviour
{
    public float speed;
    private Vector2 followSpot;
    private NavMeshAgent agent;
    private bool faceRight;

    // Start is called before the first frame update
    void Start()
    {
        followSpot = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            followSpot = new Vector2(mousePosition.x, mousePosition.y + 1f);
            updateAnimation(followSpot.x);
        }
        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));
        // transform.position = Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed);
    }

    private void updateAnimation(float followSpotX)
    {
        if (followSpotX > transform.position.x && !faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            faceRight = true;
        } else if (followSpotX < transform.position.x && faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            faceRight = false;
        }
    }
}
