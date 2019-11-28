using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadFx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 0.5초 뒤에 자신의 객체를 삭제
        Destroy(gameObject, 0.5f);
    }

}
