using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private float movementInputHorizontal = 1;
    private float movementInputVert = 0;
    private Rigidbody2D rb;
    public float movementSpeed = 4f;
    public Transform baseDot;
    public Transform someObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        InputButton();
        rb.velocity = new Vector2(movementSpeed * movementInputHorizontal, movementSpeed * movementInputVert);
        Instantiate(someObject, baseDot.transform.position, Quaternion.identity);
    }

    public void InputButton()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            movementInputHorizontal = -1;
            movementInputVert = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movementInputHorizontal = 1;
            movementInputVert = 0;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            movementInputVert = 1;
            movementInputHorizontal = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            movementInputVert = -1;
            movementInputHorizontal = 0;
        }
    }
}
