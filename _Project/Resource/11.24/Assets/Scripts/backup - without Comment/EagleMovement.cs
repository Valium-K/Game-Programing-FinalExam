using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    public float moveTime = 10.5f;
    private float tempTime = 0f;
    private bool switchDir = false;

    private void Start() {
        tempTime = moveTime;
    }
    void FixedUpdate()
    {
        if(tempTime <= 0f)
        {
            tempTime = moveTime;
            switchDir = !switchDir;
        }
        else
        {
            tempTime -= Time.deltaTime;
        }

        if(switchDir)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
        }
    }
}
