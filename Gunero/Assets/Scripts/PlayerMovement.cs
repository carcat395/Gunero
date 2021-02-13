using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Touch touch;
    public float speed = 0.1f;
    public static bool isMoving;
    bool facingRight = true;

    [SerializeField] Vector2 movementDirection;
    [SerializeField] float movementSpeed;
    [SerializeField] Vector2 playerVelocity;

    public Joystick joystick;

    Animator anim;
    Player player;
    BillboardCanvas sc;
    Rigidbody2D m_rigidbody2D;
    AudioSource audioClip;

    private void Start()
    {
        audioClip = GetComponent<AudioSource>();
        sc = GetComponentInChildren<BillboardCanvas>();
        anim = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(!GameManager.gameOver && !player.pause)
            GetInput();
    }

    void FixedUpdate()
    {
        if (!GameManager.gameOver && !player.pause)
            Move();
        else
        {
            m_rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void GetInput()
    {
        movementDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        if (movementSpeed > 0)
        {
            if(!isMoving)
                audioClip.Play();
            isMoving = true;
        }
        else
        {
            if(isMoving)
                audioClip.Stop();
            isMoving = false;
        }
    }

    void flip()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
    }

    private void Move()
    {
        m_rigidbody2D.velocity = movementDirection * movementSpeed * speed;
        playerVelocity = m_rigidbody2D.velocity;
        anim.SetFloat("Movement", movementSpeed);

        if(playerVelocity.x < 0 && facingRight)
        {
            facingRight = false;
            flip();
            sc.flip();
        }
        else if (playerVelocity.x > 0 && !facingRight)
        {
            facingRight = true;
            flip();
            sc.flip();
        }

    }
}
