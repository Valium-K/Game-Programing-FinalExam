using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int coinCount;

    public Text ScoreUI;

    [SerializeField] GameObject go_ClearUI;
    [SerializeField] GameObject go_DeathUI;

    // Start is called before the first frame update
    void Start()
    {
        coinCount = 0;
    }

    public void GetScore()
    {
        coinCount++;
        ScoreUI.text = "Score : " + (int) coinCount;
    }

    public void ShowClearUI()
    {
        go_ClearUI.SetActive(true);
    }

    public void ShowDeathUI()
    {
        go_DeathUI.SetActive(true);
    }

    public void ReStartGame()
    {
        Application.LoadLevel("test");
    }
}
