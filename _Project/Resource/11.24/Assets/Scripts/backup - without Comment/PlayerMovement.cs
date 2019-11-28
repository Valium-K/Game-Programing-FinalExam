using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerControl controller;
    public float runSpeed = 40f;
    public float dashSpeed = 0f;
    public float dashMult = 20f;
    public float maxSpeed = 20f;
    public float dashReachTime = 1.2f;
    public Animator animator;
    [SerializeField] public float hurtForce = 50f;
    public float hurtingTime = 0.3f;
    public GameObject deadFx;
    public float killJump = 500f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private bool dash = false;
    private bool isHurting = false;

    private float nowHurtingTime = 0f;

    void Update() {
        if(isHurting)
        {
            if(nowHurtingTime >= hurtingTime)
            {
                nowHurtingTime = 0f;
                isHurting = false;
                animator.SetBool("isHurting", isHurting);
            }
            else
            {
                horizontalMove = 0f;
                nowHurtingTime += Time.deltaTime;
            }
        }
        else
        {
            horizontalMove = (Input.GetAxisRaw("Horizontal") * (dashSpeed + runSpeed));// + Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }


        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }
        else if(Input.GetButtonUp("Dash"))
        {
            dash = false;
        }
        if(dash)
        {

            if (dashSpeed < maxSpeed)
            {
                dashSpeed = dashSpeed + Time.deltaTime * dashMult * 20f;
            }
            else
            {
                dashSpeed = maxSpeed;
            }

        }
        if (!dash)
        {
            if (dashSpeed >= 0f)
            {
                dashSpeed = dashSpeed - Time.deltaTime * dashMult * 35f;
            }
            else
            {
                dashSpeed = 0f;
            }
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    public void OnLanding() {
        animator.SetBool("isJumping", false);
        jump = false;
    }

    public void OnCrouching(bool inCrouching) {
        animator.SetBool("isCrouching", inCrouching);
    }

    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy")
        {
            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, killJump));
                Instantiate(deadFx, new Vector2(transform.position.x, transform.position.y - 1.2f), Quaternion.identity);
                Destroy(collision.gameObject);

            }
            else
            {
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    isHurting = true;
                    animator.SetBool("isHurting", isHurting);
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    isHurting = true;
                    animator.SetBool("isHurting", isHurting);
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }
        if(collision.gameObject.tag == "Spike")
        {
            rb.velocity = new Vector2(0f, 0f);
            Instantiate(deadFx, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            // 재시도, 끝내기 UI 추가해야함
        }
    }
}
