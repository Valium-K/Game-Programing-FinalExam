using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GetScore();
            Destroy(gameObject);
        }
    }
}
