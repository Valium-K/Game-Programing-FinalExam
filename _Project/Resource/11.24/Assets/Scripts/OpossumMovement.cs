using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumMovement : MonoBehaviour
{
    public float moveTime = 10f;
    private float tempTime = 0f;
    private bool switchDir = false;

    // EagleMovement.cs에서 y축 대신 x축으로 이동하는것.
    // EagleMovement 스크립트를 보면 됨
    private void Start() {
        tempTime = moveTime;
    }
    void FixedUpdate() {
        if (tempTime <= 0f)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            tempTime = moveTime;
            switchDir = !switchDir;
        }
        else
        {
            tempTime -= Time.deltaTime;
        }

        if (switchDir)
        {
            transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
        }
    }
}
