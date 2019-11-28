using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    public float moveTime = 10.5f;
    private float tempTime = 0f;
    private bool switchDir = false;

    private void Start() {
        //독수리의 방향을 바꾸기 까지 걸리는 시간을 임시로 저장
        tempTime = moveTime;
    }
    void FixedUpdate()
    {
        // 시간이 다 되면
        if(tempTime <= 0f)
        {
            //시간을 다시 설정한 후
            tempTime = moveTime;
            // 방향을 바꾼다.
            switchDir = !switchDir;
        }
        // 방향을 바꾸기 까지 시간이 남으면
        else
        {
            // 시간을 계속 줄인다.
            tempTime -= Time.deltaTime;
        }

        // switchDir에 따라 방향을 바꾼다.
        if(switchDir)
        {
            // 이 오브젝트의 y 포지션을 0.1만큼 증가시킨다.
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        }
        else
        {
            // 이 오브젝트의 y 포지션을 0.1만큼 감소시킨다.
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
        }
    }
}
