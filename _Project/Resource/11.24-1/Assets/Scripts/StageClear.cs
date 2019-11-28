using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] GameManager gm;
    //private Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //animator.SetTrigger("StageClear");
            gm.ShowClearUI();
        }
    }
}
