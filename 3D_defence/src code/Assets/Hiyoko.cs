using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko : MonoBehaviour
{
    public float power = 6.5f;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("DeathZone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direction = target.transform.position - transform.position;
        GetComponent< Rigidbody > ().AddForce(direction.normalized * power);
        transform.LookAt(direction);
    }
}
