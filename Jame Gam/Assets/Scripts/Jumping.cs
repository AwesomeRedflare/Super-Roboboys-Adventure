using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float jumpForce;

    private Rigidbody2D rb;

    public int jumpIntervals;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("Jump", jumpIntervals, jumpIntervals);
    }

    void Jump()
    {
        int jumpChance;

        jumpChance = Random.Range(0, 2);

        Debug.Log(jumpChance);

        if(jumpChance == 1)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
