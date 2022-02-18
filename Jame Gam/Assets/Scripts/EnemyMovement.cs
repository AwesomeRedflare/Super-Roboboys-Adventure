using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float eyeSight;
    public float stopDistance = 0.1f;

    private Transform playerPos;

    public bool facingRight = true;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(playerPos.position, transform.position) < eyeSight)
        {
            if (playerPos.position.x > transform.position.x && playerPos.position.x - transform.position.x > stopDistance)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (facingRight == true)
                {
                    transform.Rotate(0, 180, 0);
                    //transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    facingRight = false;
                }
            }

            if (playerPos.position.x < transform.position.x && transform.position.x - playerPos.position.x > stopDistance)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (facingRight == false)
                {
                    transform.Rotate(0, 180, 0);
                   //transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    facingRight = true;
                }
            }
        }
    }
}
