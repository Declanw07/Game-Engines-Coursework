using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip[] blockSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayBlockSound(int blockType){

		//audio.PlayOneShot(blockSound[blockType]);

	}

	void OnEnable(){

		VoxelChunk.OnEventBlockChanged +=
			PlayBlockSound;

	}

	void OnDisable(){

		VoxelChunk.OnEventBlockChanged -=
			PlayBlockSound;

	}

}
