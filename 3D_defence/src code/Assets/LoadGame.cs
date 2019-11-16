using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGamefunction()
    {
        UIController.gameOver = false;
        SceneManager.LoadScene("SampleScene");
        Score.score = 0;
    }
}
