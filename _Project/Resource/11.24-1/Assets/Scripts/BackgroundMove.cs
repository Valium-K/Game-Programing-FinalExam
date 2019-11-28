using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Transform playerPos;
    public float bgSpeed;
    public Renderer bgRend;

    private void FixedUpdate() {
        // 이 오브젝트의 transform의 position을 플레이어의 x, y로 할당해서 플레이어 위치로 계속 따라다니게 만듦.
        transform.position = new Vector2(playerPos.position.x, transform.position.y);

        // 플레이어의 x속도에 기반해서 백그라운드를 스크롤 하게 만듦.
        bgRend.material.mainTextureOffset += new Vector2(playerRB.velocity.x * bgSpeed * Time.deltaTime, 0f); 
    }
}
