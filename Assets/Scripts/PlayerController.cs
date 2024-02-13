using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float brakPower;
    [SerializeField] float MaxXSpeed;
    [SerializeField] float MaxYSpeed;

    [SerializeField] float jumpSpeed;


    private Vector2 moverDir;
    private bool isGround;
    private int index;

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (moverDir.x < 0 && rigid.velocity.x > -MaxXSpeed)
        {
            rigid.AddForce(Vector2.right * moverDir.x * movePower);
        }
        else if (moverDir.x > 0 && rigid.velocity.x < MaxXSpeed)
        {
            rigid.AddForce(Vector2.right * moverDir.x * movePower);
        }
        else if (moverDir.x == 0 && rigid.velocity.x > 0.1f)
        {
            rigid.AddForce(Vector2.left * brakPower);
        }
        else if (moverDir.x == 0 && rigid.velocity.x < -0.1f)
        {
            rigid.AddForce(Vector2.right * brakPower);
        }

        if (rigid.velocity.y < -MaxYSpeed)
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = -MaxYSpeed;
            rigid.velocity = velocity;
        }

        animator.SetFloat("YSpeed", rigid.velocity.y);

    }
    private void Jump()
    {
        Vector2 velocity = rigid.velocity;
        velocity.y = jumpSpeed;
        rigid.velocity = velocity;
    }
    private void OnMove(InputValue value)
    {
        moverDir = value.Get<Vector2>();
        if (moverDir.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else if (moverDir.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGround && index != 0)
        {
            Jump();
            index--;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        index = 2;


        isGround = true;


    }
    /*private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }*/
}
