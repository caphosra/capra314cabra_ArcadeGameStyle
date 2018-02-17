using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    public Text text;

	// Use this for initialization
	void Start () {
        text.text = "Score : " + System.String.Format("{0:D7}", ScoreManager.Score);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
