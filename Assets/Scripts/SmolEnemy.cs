using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolEnemy : MonoBehaviour
{

    private Vector2 directionToPlayer;
    Rigidbody2D rb;
    public float moveSpeed = 1;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        directionToPlayer = (new Vector3(0, 0, 0) - transform.position).normalized;

        rb.AddForce(directionToPlayer*moveSpeed);

    }
    void SpeedUp()
    {
        if (moveSpeed < 10)
        {
            moveSpeed += 0.5f;
        }
    }
}
