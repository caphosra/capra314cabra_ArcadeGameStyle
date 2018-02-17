using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartCount : MonoBehaviour
{
    public Text countText;

    public string stageName;
    public float stage_Time;
    public float ready_Time;
    public float go_Time;

    private float time;

    // Use this for initialization
    void Start()
    {
        time = 0;
        countText.text = stageName;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > (stage_Time + ready_Time + go_Time))
        {
            GameFunc.PauseObjects(GameObject.FindGameObjectsWithTag("Player"), true);
            GameFunc.PauseObjects(GameObject.FindGameObjectsWithTag("Enemy"), true);
            Destroy(gameObject);
        }
        else
        {
            if(time > (stage_Time + ready_Time))
            {
                countText.text = "GO!";
            }
            else
            {
                if(time > (stage_Time))
                {
                    countText.text = "READY";
                }
            }
        }
        time += Time.deltaTime;
    }
}
