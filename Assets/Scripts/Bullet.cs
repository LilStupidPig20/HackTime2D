using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Transform Player;
    public int damage = 1;
    /*

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        hit = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    */
}
