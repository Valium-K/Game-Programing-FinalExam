using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject prefab;
    public float power;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var bullet = LoadBullet();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = ray.direction.normalized;

            bullet.GetComponent< Rigidbody > ().velocity = dir * power;
        }
    }

    GameObject LoadBullet()
    {
        var bullet = GameObject.Instantiate(prefab) as GameObject;

        bullet.transform.parent = transform;
        bullet.transform.localPosition = Vector3.zero;

        return bullet;
    }
}
