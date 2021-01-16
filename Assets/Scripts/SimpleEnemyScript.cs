using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public int health,
        positionOfPatrol;

    private Transform
        player,
        point,
        checkLowLet;

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
        movingRight = true,
        patrol = false,
        angry = false,
        goback = false,
        goingToLastPosOfPlayer = false,
        patrolLastPosOfPlayer = false,
        groundDetected,
        wallDetected;

    private Transform
        groundCheck,
        wallCheck;

    private float
        groundCheckDistance,
        wallCheckDistance,
        timeBetweenShots;

    public Vector2 lastPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweeenShots;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if ((transform.position.x < point.position.x + positionOfPatrol) || (transform.position.x > point.position.x - positionOfPatrol) && (angry == false))
        {
            patrol = true;
        }

        if (movingRight == true)
        {
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, Vector2.right, triggerDistance, Player);
            RaycastHit2D hitLet = Physics2D.Raycast(transform.position, Vector2.right, triggerDistance, LetForEnemy);
            RaycastHit2D hitLowLet = Physics2D.Raycast(checkLowLet.transform.position, Vector2.right, checkLowLetDistance, LowLetForEnemy);

            Debug.DrawLine(this.transform.position, Vector2.right * triggerDistance, Color.red);

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
                    patrolLastPosOfPlayer = false;
                    goingToLastPosOfPlayer = false;
                }
            }
            else if (patrolLastPosOfPlayer == false && goback == false)
            {
                goingToLastPosOfPlayer = true;
                angry = false;
            }
            else if (goingToLastPosOfPlayer == false && goback == false && timer > 0)
            {
                timer -= Time.deltaTime;
                angry = false;
                patrolLastPosOfPlayer = true;
            }
            else if (goingToLastPosOfPlayer == false && patrolLastPosOfPlayer == false)
            {
                timer = 10;
                angry = false;
                goback = true;
            }

            if (hitLowLet.collider != null)
            {
                Jump();
            }

            if (patrol == true)
            {
                Patrol();
            }
            else if (angry == true)
            {
                Angry();
              
            }
            else if (goingToLastPosOfPlayer == true)
            {
                GoingToLastPosOfPlayer(lastPos);
            }
            else if (patrolLastPosOfPlayer == true)
            {
                PatrolingLastPosOfPlayer(lastPos);
            }
            else if (goback == true)
            {
                GoBack();
            }
        }
        else
        {
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, Vector2.left, triggerDistance, Player);
            RaycastHit2D hitLet = Physics2D.Raycast(transform.position, Vector2.left, triggerDistance, LetForEnemy);
            RaycastHit2D hitLowLet = Physics2D.Raycast(checkLowLet.transform.position, Vector2.left, checkLowLetDistance, LowLetForEnemy);

            Debug.DrawLine(this.transform.position, Vector2.left * triggerDistance, Color.red);

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
            else if (patrolLastPosOfPlayer == false && goback == false)
            {
                goingToLastPosOfPlayer = true;
                angry = false;
            }
            else if (goingToLastPosOfPlayer == false && goback == false && timer > 0)
            {
                timer -= Time.deltaTime;
                angry = false;
                patrolLastPosOfPlayer = true;
            }
            else if (goingToLastPosOfPlayer == false && patrolLastPosOfPlayer == false)
            {
                timer = 10;
                angry = false;
                goback = true;
            }

            if (hitLowLet.collider != null)
            {
                Jump();
            }

            if (patrol == true)
            {
                Patrol();
            }
            else if (angry == true)
            {
                Angry();
               
            }
            else if (goingToLastPosOfPlayer == true)
            {
                GoingToLastPosOfPlayer(lastPos);
            }
            else if (patrolLastPosOfPlayer == true)
            {
                PatrolingLastPosOfPlayer(lastPos);
            }
            else if (goback == true)
            {
                GoBack();
            }
        }
    }

    void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

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


    void Jump()
    {
        rb.velocity = Vector2.up * 5;
    }

    void GoingToLastPosOfPlayer(Vector2 lastPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, lastPos, speed * Time.deltaTime);

    }

    void PatrolingLastPosOfPlayer(Vector2 lastPos)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > lastPos.x + positionOfPatrol)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else if (transform.position.x < lastPos.x - positionOfPatrol)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
