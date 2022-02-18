using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    public int health;
    public int damage;

    private Animator anim;

    public GameObject hitPartcile;

    private EnemyMovement enemyMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            enemyMovement.speed = 0;
            Destroy(gameObject, .5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Gold"))
        {
            Destroy(col.gameObject);
            health--;
        }

        if (col.gameObject.CompareTag("Gold") && health == 0)
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
        }

        if (col.gameObject.CompareTag("bullet"))
        {
            health = 0;
            FindObjectOfType<AudioManager>().Play("Explosion");
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("feet"))
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            Instantiate(hitPartcile, transform.position, transform.rotation);
            col.gameObject.GetComponentInParent<PlayerController>().rb.velocity = Vector2.up * (col.gameObject.GetComponentInParent<PlayerController>().jumpForce * .8f);
            health--;
        }

        if(col.gameObject.CompareTag("feet") && health == 0)
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
        }
    }
}
