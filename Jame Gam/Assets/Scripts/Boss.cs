using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public EnemyMovement enemyMovement;

    public GameManager gameManager;

    public int health;
    public int damage;

    private Animator anim;

    public GameObject hitPartcile;
    public GameObject enemy;
    public GameObject smallEnemy;

    public float speed;

    public Transform pointOne;
    public Transform pointTwo;

    public bool attackPhase = false;
    public bool fight = false;

    public bool destinationOne;

    private int t = 0;
    public int turns;
    private int s = 0;
    private int se = 0;
    public int enemySpawnTurn;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            enemyMovement.speed = 0;
        }

        if(attackPhase == true && health >= 1)
        {
            AttackPhase();
        }
        
        if(attackPhase == false && fight == true)
        {
            anim.SetBool("isWalking", true);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            GetComponent<BoxCollider2D>().enabled = true;
            enemyMovement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("feet"))
        {
            t = 0;
            s = 0;
            se = 0;
            attackPhase = true;
            FindObjectOfType<AudioManager>().Play("Jump");
            Instantiate(hitPartcile, transform.position, transform.rotation);
            col.gameObject.GetComponentInParent<PlayerController>().rb.velocity = Vector2.up * (col.gameObject.GetComponentInParent<PlayerController>().jumpForce * .8f);
            health--;
        }

        if (col.gameObject.CompareTag("feet") && health == 0)
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
            gameManager.Win();
        }
    }

    void AttackPhase()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;
        enemyMovement.enabled = false;
        anim.SetBool("isWalking", false);

        if (destinationOne == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointOne.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointTwo.position, speed * Time.deltaTime);
        }

        if (transform.position == pointOne.position)
        {
            destinationOne = false;
            t++;
        }

        if (transform.position == pointTwo.position)
        {
            destinationOne = true;
            t++;
        }

        if(t == turns)
        {
            attackPhase = false;
        }

        if(health == 2)
        {
            SpawnEnemies();
        }

        if(health == 4 || health == 3)
        {
            SpawnSmallEnemies();
        }

        if(health == 1)
        {
            SpawnEnemies();
            SpawnSmallEnemies();
        }
    }

    void SpawnEnemies()
    {
        if ((t == turns / enemySpawnTurn && s == 0) || (t == (turns / enemySpawnTurn) * 2 && s == 1) || (t == ((turns / enemySpawnTurn) * 3) - 1 && s == 2))
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            s++;
        }
    }

    void SpawnSmallEnemies()
    {
        if ((t == (turns / enemySpawnTurn) - 1 && se == 0) || (t == ((turns / enemySpawnTurn) * 2) - 1 && se == 1) || (t == ((turns / enemySpawnTurn) * 3) - 2 && se == 2))
        {
            Instantiate(smallEnemy, transform.position, Quaternion.identity);
            se++;
        }
    }
}
