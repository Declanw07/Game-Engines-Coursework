using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public delegate void EventChangeBlock(Vector3 v, int newBlock);
	public static event EventChangeBlock OnEventChangeBlock;
	public delegate void UpdateInventory(int[] blockAmount);
	public static event UpdateInventory OnEventUpdateInventory; 

	public int[] blocksHeld;

	int blockSelected = 1;

	bool destroy;
	public bool isScene3;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.Log ("Grass Held: " + blocksHeld [0]);
//		Debug.Log ("Dirt Held: " + blocksHeld [1]);
//		Debug.Log ("Stone Held: " + blocksHeld [2]);
//		Debug.Log ("Sand Held: " + blocksHeld [3]);


		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			blockSelected = 1;
			Debug.Log (blockSelected);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			blockSelected = 2;
			Debug.Log (blockSelected);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			blockSelected = 3;
			Debug.Log (blockSelected);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			blockSelected = 4;
			Debug.Log (blockSelected);
		}

		if(Input.GetButtonDown("Fire1")){
//			Debug.Log ("Detected Mouse Click");
			Vector3 v;
			if(PickBlock(out v, 4, true))
			{
//				Debug.Log ("Destroying Block");
				OnEventChangeBlock(v, 0);

			}
		}
		else if(Input.GetButtonDown("Fire2") && blocksHeld[blockSelected-1] > 0){
			Debug.Log ("Detected Mouse 2 Click");
			Vector3 v;
			if(PickBlock(out v, 4, false))
			{
				Debug.Log (v);
				OnEventChangeBlock(v, blockSelected);
				blocksHeld[blockSelected-1]--;
				OnEventUpdateInventory(blocksHeld);

			}
		}
	}

	bool PickBlock(out Vector3 v, float dist, bool destroy)
	{
		v = new Vector3();
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(
			Screen.width/2,Screen.height/2,0));
		
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, dist))
		{
			Debug.Log ("Raycast Hit");

			v = destroy ? hit.point-hit.normal/2 : hit.point+hit.normal/2;

			// round down to get the index of the block hit
			v.x = Mathf.Floor(v.x);
			v.y = Mathf.Floor(v.y);
			v.z = Mathf.Floor(v.z);
			return true;
		}
		return false;
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "GrassBlockPrefab(Clone)") {
			blocksHeld[0]++;
			OnEventUpdateInventory(blocksHeld);
			Destroy(col.gameObject);
		}
		if (col.gameObject.name == "DirtBlockPrefab(Clone)") {
			blocksHeld[1]++;
			OnEventUpdateInventory(blocksHeld);
			Destroy(col.gameObject);
		}
		if (col.gameObject.name == "StoneBlockPrefab(Clone)") {
			blocksHeld[2]++;
			OnEventUpdateInventory(blocksHeld);
			Destroy(col.gameObject);
		}
		if (col.gameObject.name == "SandBlockPrefab(Clone)") {
			blocksHeld[3]++;
			OnEventUpdateInventory(blocksHeld);
			Destroy(col.gameObject);
		}


	}
	
}
