using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
}
