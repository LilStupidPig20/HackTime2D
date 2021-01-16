using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int damage = 101;

    private Transform player;
    private Vector2 target;

    private bool right;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        if (player.transform.position.x > transform.position.x)
        {
            right = true;
        }
        else
        {
            right = false;
        }
    }

    void Update()
    {
        if (right == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        PlayerController playerhit = collision.GetComponent<PlayerController>();
        if (playerhit != null)
        {
            playerhit.TakeDamage(damage);
        }
        Destroy(gameObject);
        */

        if (collision.CompareTag("Player"))
        {
            var tupaPlaya = GameObject.FindGameObjectWithTag("Player");
            tupaPlaya.GetComponent<PlayerController>().TakeDamage(damage);
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
