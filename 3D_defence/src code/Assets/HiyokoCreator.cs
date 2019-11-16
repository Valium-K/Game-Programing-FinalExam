using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiyokoCreator : MonoBehaviour
{
    public GameObject obj;
    public float interval = 3;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DeathZone.dead == false)
        {
            time += Time.deltaTime;

            if (time >= interval)
            {
                time = 0;
                GameObject hiyoko = Instantiate(obj) as GameObject;

                hiyoko.transform.localPosition = new Vector3(
                    Random.Range(-3.0f, 3.0f),
                    Random.Range(1.0f, 5.0f),
                    Random.Range(17.0f, 20.0f));

                Score.score++;
            }
        }
    }
}
