using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public int health = 100,
        movingRight = 1,
        positionOfPatrol,
        damage;

    public Transform
        player,
        point,
        checkLowLet,
        wallCheck,
        groundCheck;

    public LayerMask
        Player,
        LetForEnemy,
        LowLetForEnemy,
        whatIsGround;

    public float
        startTimeBetweeenShots,
        triggerDistance,
        speed,
        timer,
        stoppingDistance,
        checkLowLetDistance;

    public GameObject bullet;

    private bool
        patrol = false,
        angry = false,
        goback = false,
        groundDetected,
        wallDetected;

    public float
        groundCheckDistance,
        wallCheckDistance,
        timeBetweenShots;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweeenShots;
    }

    private void Update()
    {
        WallCheck();
        GroundCheck();

        if ((transform.position.x < point.position.x + positionOfPatrol) || (transform.position.x > point.position.x - positionOfPatrol) && (angry == false))
        {
            patrol = true;
        }

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.right, triggerDistance, Player);
        RaycastHit2D hitLet = Physics2D.Raycast(transform.position, transform.right, triggerDistance, LetForEnemy);
        RaycastHit2D hitLowLet = Physics2D.Raycast(checkLowLet.transform.position, transform.right, checkLowLetDistance, LowLetForEnemy);

        if (hitLowLet.collider != null)
        {
            Jump();
        }

        if (hitPlayer.collider != null)
        {
            if (hitPlayer.distance > hitLet.distance && hitLet.collider != null)
            {
                angry = false;
                Debug.Log("Путь к врагу преграждает объект: " + hitLet.collider.name);
            }
            else
            {
                Debug.Log("Атакую игрока!!!");
                angry = true;
                patrol = false;
                goback = false;
            }
        }
        else
        {
            goback = true;
            angry = false;
        }

        if (patrol == true)
        {
            Patrol();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goback == true)
        {
            GoBack();
        }
    }

    void Patrol()
    {
        if (positionOfPatrol == 0)
        {
            timer++;
            if (timer >= 200)
            {
                timer = 0;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x > point.position.x + positionOfPatrol)
            {
                Flip();
            }
            else if (transform.position.x < point.position.x - positionOfPatrol)
            {
                Flip();
            }
        }
    }

    void Angry()
    {
        if (movingRight == 1)
        {
            if (transform.position.x > player.position.x - stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.x > player.position.x + stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }

        if (timeBetweenShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweeenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
    
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    void WallCheck()
    {
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        if (wallDetected != false)
        {
            Flip();
        }
    }

    void GroundCheck()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        if (groundDetected == false)
        {
            Flip();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * 5;
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        movingRight *= -1;
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            return;
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
