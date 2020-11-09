using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    public bool movingRight = true;
    public int positionOfPatrol;
    public float stoppingDistance;

    private Transform player;
    public Transform point;
    public Transform checkLowLet;

    public float triggerDistance;
    public float checkLowLetDistance;

    public LayerMask Player;
    public LayerMask LetForEnemy;
    public LayerMask LowLetForEnemy;

    private float timeBetweenShots;
    public float startTimeBetweeenShots;
    public GameObject bullet;

    private bool patrol = false;
    private bool angry = false;
    private bool goback = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweeenShots;
    }

    void Update()
    {      
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
                if (hitPlayer.distance > hitLet.distance)
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

            if (hitLowLet.collider != null)
            {
                Jump();
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
                if (hitPlayer.distance > hitLet.distance)
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

            if (hitLowLet.collider != null)
            {
                Jump();
            }
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

    void Flip()                   
    {
        transform.Rotate(0f, 180f, 0f);
    }

    /*
    private void OnDrawGizmos()
    {
        float angle = 90 * Mathf.Deg2Rad;
        var dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * dir * checkLowLetDistance);
    }
    */
}

