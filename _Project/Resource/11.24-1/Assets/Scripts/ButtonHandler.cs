using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void SetReStart()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().ReStartGame();
    }

    public void SetNextStage()
    {
        Debug.Log("다음 스테이지");
    }

    public void SetExit()
    {
        Debug.Log("게임 종료");
    }
}
