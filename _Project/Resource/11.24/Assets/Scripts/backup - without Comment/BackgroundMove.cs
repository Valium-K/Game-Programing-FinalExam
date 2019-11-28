using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Rigidbody2D player;
    public Transform playerPos;
    public Transform bgPos;
    public float bgSpeed;
    public Renderer bgRend;

    private void FixedUpdate() {
        bgPos.position = new Vector2(playerPos.position.x, bgPos.position.y);
        bgRend.material.mainTextureOffset += new Vector2(player.velocity.x * bgSpeed * Time.deltaTime, 0f);
    }
}
