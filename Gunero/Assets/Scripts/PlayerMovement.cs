using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Touch touch;
    public float speed = 0.1f;
    public static bool isMoving;

    [SerializeField] Vector2 movementDirection;
    [SerializeField] float movementSpeed;
    [SerializeField] Vector2 playerVelocity;

    public Joystick joystick;

    Rigidbody2D m_rigidbody2D;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!GameManager.gameOver)
            GetInput();
    }

    void FixedUpdate()
    {
        if(!GameManager.gameOver)
            Move();
    }

    private void GetInput()
    {
        movementDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        if (movementSpeed > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void Move()
    {
        m_rigidbody2D.velocity = movementDirection * movementSpeed * speed;
        playerVelocity = m_rigidbody2D.velocity;
    }
}
