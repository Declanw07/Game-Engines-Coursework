using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadScene1(){
		Application.LoadLevel("Scene 1 - Voxel Scene");
	}
	public void loadScene2(){
		Application.LoadLevel("Scene 2 - Pathfinding");
	}
	public void loadScene3(){
		Application.LoadLevel("Scene 3 - Networking Scene");
	}
}
