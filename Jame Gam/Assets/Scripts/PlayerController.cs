using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedIncrements;
    public float minSpeed;
    public float maxSpeed;
    public float jumpForce;
    public float jumpIncrements;
    public float minJumpForce;
    public float maxJumpForce;
    private float moveInput;

    public float knockback;
    public float knockbackLenght;
    private float knockbackCount;
    public bool knockFromRight;

    public Rigidbody2D rb;

    private bool facingRight = true;

    private bool hasWon = false;
    public bool canMove = true;

    private bool isHurt = false;
    public float invulnerableDuration;
    private float invulnerableCountDown;
    private Renderer rend;
    private Color c;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    public static int goldAmount = 0;
    public Transform projectilePoint;
    public GameObject goldProjectile;
    public GameObject goldParticle;
    public GameObject collectParticle;

    public Animator armAnim;
    public Animator legAnim;

    public GameManager gameManager;

    private void Start()
    {
        CalculateStats();
        extraJumps = extraJumpValue;
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(canMove == true)
        {
            if (hasWon == false)
            {
                if (knockbackCount <= 0)
                {
                    moveInput = Input.GetAxisRaw("Horizontal");
                    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
                }
                else
                {
                    if (knockFromRight)
                        rb.velocity = new Vector2(-knockback, knockback);
                    if (!knockFromRight)
                        rb.velocity = new Vector2(knockback, knockback);

                    knockbackCount -= Time.deltaTime;
                }
            }
        }


        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
        
        if(moveInput == 0)
        {
            armAnim.SetBool("isWalking", false);
            legAnim.SetBool("isWalking", false);
        }
        else
        {
            armAnim.SetBool("isWalking", true);
            legAnim.SetBool("isWalking", true);
        }
    }

    private void Update()
    {
        if(isGrounded == true)
        {
            extraJumps = extraJumpValue;
            armAnim.SetBool("isJumping", false);
            legAnim.SetBool("isJumping", false);
        }
        else
        {
            armAnim.SetBool("isJumping", true);
            legAnim.SetBool("isJumping", true);
        }

        if(canMove == true)
        {
            if (hasWon == false)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    extraJumps--;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
                {
                    FindObjectOfType<AudioManager>().Play("Jump");
                    rb.velocity = Vector2.up * jumpForce;
                }

                if (Input.GetKeyDown(KeyCode.Space) && goldAmount > 0)
                {
                    goldAmount--;

                    CalculateStats();

                    Instantiate(goldProjectile, projectilePoint.position, transform.rotation);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Death();
                }
            }
        }


        if (isHurt == true && invulnerableCountDown > 0)
        {
            c.a = 0.5f;
            rend.material.color = c;
            invulnerableCountDown -= Time.deltaTime;
        }
        if(invulnerableCountDown <= 0)
        {
            c.a = 1f;
            rend.material.color = c;
            isHurt = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Gold"))
        {
            FindObjectOfType<AudioManager>().Play("Collect");
            Destroy(col.gameObject);
            Instantiate(collectParticle, transform.position, transform.rotation);
            goldAmount++;

            CalculateStats();
        }

        if (col.CompareTag("LevelComplete"))
        {
            LevelComplete();
        }

        if (col.CompareTag("door"))
        {
            gameManager.Door();
            canMove = false;
            gameObject.SetActive(false);
            moveInput = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Gold"))
        {
            FindObjectOfType<AudioManager>().Play("Collect");
            Destroy(col.gameObject);
            Instantiate(collectParticle, transform.position, transform.rotation);
            goldAmount++;
            if (speed < maxSpeed)
            {
                speed += speedIncrements;
            }

            if(jumpForce < maxJumpForce)
            {
                jumpForce += jumpIncrements;
            }
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            if(isHurt == false)
            {
                FindObjectOfType<AudioManager>().Play("Hurt");

                if (goldAmount == 0)
                {
                    Death();
                }
                else if (goldAmount > 0)
                {
                    isHurt = true;

                    for (int i = 0; i < col.gameObject.GetComponent<SmallEnemy>().damage; i++)
                    {
                        Instantiate(goldParticle, transform.position, Quaternion.identity);
                        goldAmount--;
                    }

                    //knockbackstuff
                    knockbackCount = knockbackLenght;

                    if (transform.position.x < col.transform.position.x) 
                        knockFromRight = true;
                    else
                        knockFromRight = false;

                    CalculateStats();
                }

                if (goldAmount < 0)
                {
                    goldAmount = 0;
                }

                invulnerableCountDown = invulnerableDuration;
            }
        }

        if (col.gameObject.CompareTag("bullet"))
        {
            if (isHurt == false)
            {
                Destroy(col.gameObject);

                FindObjectOfType<AudioManager>().Play("Hurt");

                if (goldAmount == 0)
                {
                    Death();
                }
                else if (goldAmount > 0)
                {
                    isHurt = true;

                    for (int i = 0; i < col.gameObject.GetComponent<Bullet>().damage; i++)
                    {
                        Instantiate(goldParticle, transform.position, Quaternion.identity);
                        goldAmount--;
                    }

                    //knockbackstuff
                    knockbackCount = knockbackLenght;

                    if (transform.position.x < col.transform.position.x)
                        knockFromRight = true;
                    else
                        knockFromRight = false;

                    CalculateStats();
                }

                if (goldAmount < 0)
                {
                    goldAmount = 0;
                }

                invulnerableCountDown = invulnerableDuration;
            }
        }

        if (col.gameObject.CompareTag("boss"))
        {
            if (isHurt == false)
            {
                FindObjectOfType<AudioManager>().Play("Hurt");

                if (goldAmount == 0)
                {
                    Death();
                }
                else if (goldAmount > 0)
                {
                    isHurt = true;

                    for (int i = 0; i < col.gameObject.GetComponent<Boss>().damage; i++)
                    {
                        Instantiate(goldParticle, transform.position, Quaternion.identity);
                        goldAmount--;
                    }

                    //knockbackstuff
                    knockbackCount = knockbackLenght;

                    if (transform.position.x < col.transform.position.x)
                        knockFromRight = true;
                    else
                        knockFromRight = false;

                    CalculateStats();
                }

                if (goldAmount < 0)
                {
                    goldAmount = 0;
                }

                invulnerableCountDown = invulnerableDuration;
            }
        }

        if (col.gameObject.CompareTag("death"))
        {
            Death();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    void LevelComplete()
    {
        hasWon = true;
        moveInput = .5f;
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        gameManager.LevelComplete();
        //play animation
    }

    void Death()
    {
        FindObjectOfType<AudioManager>().Mute("Theme");
        FindObjectOfType<AudioManager>().Play("Death");
        goldAmount = 0;
        GetComponent<Animator>().SetTrigger("dead");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        gameManager.Transition();
        gameManager.Invoke("Death", 1.2f);
    }

    void CalculateStats()
    {
        speed = minSpeed;
        jumpForce = minJumpForce;

        for (int i = 0; i < goldAmount; i++)
        {
            if (speed < maxSpeed)
            {
                speed += speedIncrements;
            }

            if (jumpForce < maxJumpForce)
            {
                jumpForce += jumpIncrements;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
