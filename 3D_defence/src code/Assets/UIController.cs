using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text text;
    public static bool gameOver;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            text.enabled = true;
            button.SetActive(true);
        }
    }
}
