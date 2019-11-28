using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; // 이 오브젝트의 리지드바디
    public PlayerControl controller; // 플레이어 컨트롤러 클래스를 불러온다.
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
        // 맞고있는 상태라면
        if(isHurting)
        {
            // 맞는 모션 + 넉백동안 입력을 방지하기 위해 hurtingTime이 될때까지 nowHurtingTime을 증가시키다가 조건이 될경우
            if(nowHurtingTime >= hurtingTime)
            {
                // 입력을 다시 받을 수 있게 상태를 바꿔줌
                nowHurtingTime = 0f;
                isHurting = false;
                animator.SetBool("isHurting", isHurting);
            }
            else
            {
                // 아직 hurtingTIme까지 도달하지 않았을경우 계속 연산을 함
                horizontalMove = 0f;
                nowHurtingTime += Time.deltaTime;
            }
        }
        // 맞고있지 않다면
        else
        {
            // 플레이어 입력을 받음
            horizontalMove = (Input.GetAxisRaw("Horizontal") * (dashSpeed + runSpeed));// 수평 축 * (대시 속도 + 기본속도)의 값을 저장
            // 이동모션을 재생하기 위해 animator의 Speed 변수의 값을 넘겨줌
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        
        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        // 대시키를 누르는 동안 계속 속도를 증가해야 하기에 키 다운시에 dash를 true로 바꿈
        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }

        // 때면 flase로 바꿈
        else if(Input.GetButtonUp("Dash"))
        {
            dash = false;
        }

        // 대시키가 눌린경우
        if(dash)
        {
            // 대시로 얻는 최대 속도에 도달하지 않았을 경우
            if (dashSpeed < maxSpeed)
            {
                // 대시를 할껀데 서서히 속도을 더해감
                dashSpeed = dashSpeed + Time.deltaTime * dashMult * 20f;
            }
            // 최대 속도면
            else
            {
                // 계속 유지하기
                dashSpeed = maxSpeed;
            }

        }

        // 대시키가 때였을 경우
        if (!dash)
        {
            // 아직 대시 속도가 남았으면
            if (dashSpeed >= 0f)
            {
                // 계속 속도를 줄여줌
                dashSpeed = dashSpeed - Time.deltaTime * dashMult * 35f;
            }
            // 대시 속도가 끝나면
            else
            {
                //끝난 것이다.
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

    // 이 함수는 playerControl.cs의 이벤트가 직접 호출을 함
    public void OnLanding() {
        animator.SetBool("isJumping", false);
        jump = false;
    }

    // 이 함수는 playerControl.cs의 이벤트가 직접 호출함
    public void OnCrouching(bool inCrouching) {
        animator.SetBool("isCrouching", inCrouching);
    }

    // 플레이어 상태값을 playerControl.cs의 move()함수에 넘겨줌
    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }

    // 플레이어가 콜리전 충돌이 나면
    private void OnCollisionEnter2D(Collision2D collision) {
        // 적(몬스터)인인데
        if (collision.gameObject.tag == "Enemy")
        {
            // 점프로 밟아 충돌한경우
            if (jump)
            {
                //속도를 순간 멈추고
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                // killJump의 힘만큼 위로 올라감
                rb.AddForce(new Vector2(0f, killJump));

                // 그리고 deadFx를 플레이어 약간 밑에다가 동적할당함. deadFx는 단순히 죽는 애니메이션의 스프라이트만 있음
                Instantiate(deadFx, new Vector2(transform.position.x, transform.position.y - 1.2f), Quaternion.identity);

                //그리고 적을 없앰
                Destroy(collision.gameObject);

            }
            // 밟은게 아니라면
            else
            {
                // 적이 내 오른쪽에 있는채로 충돌난거였으면
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    // 맞고 
                    isHurting = true;
                    // 애니메이션 내고
                    animator.SetBool("isHurting", isHurting);
                    //왼쪽으로 약간 넉백
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                // 적이 왼쪽이였으면
                else
                {
                    // 맞고 애니메이션 내고 오른쪽으로 약간 넉백
                    isHurting = true;
                    animator.SetBool("isHurting", isHurting);
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }

        // 만약 가시와 충돌했으면
        if(collision.gameObject.tag == "Spike")
        {
            // 즉시 이동을 멈추고
            rb.velocity = new Vector2(0f, 0f);
            // 죽는 애니메이션 내고
            Instantiate(deadFx, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            // 플레이어 이동 스크립트가있는 PlayerMovement를 해제
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            // 플레이어 스프라이트를 해제
            gameObject.GetComponent<SpriteRenderer>().enabled = false;


            // 여기에 재시도, 끝내기 UI 추가해야함
        }
    }
}
