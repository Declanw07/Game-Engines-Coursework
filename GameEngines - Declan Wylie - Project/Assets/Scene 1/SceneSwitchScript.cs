using UnityEngine;
using System.Collections;

public class SceneSwitchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadMainMenu(){
		Application.LoadLevel("MainMenu");
	}

}
