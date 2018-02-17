using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirstStage : MonoBehaviour {

    public string firstStage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space)) UnityEngine.SceneManagement.SceneManager.LoadScene(firstStage);
	}
}
