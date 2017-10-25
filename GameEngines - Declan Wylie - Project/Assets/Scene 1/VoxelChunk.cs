using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoxelChunk : MonoBehaviour {
	
	VoxelGenerator voxelGenerator;
	int[, ,] terrainArray;
	int chunkSize = 16;
	List<Vector3> stoneNodes;

	public int cost;

	string currentLevel;

	public GameObject[] blockPrefabs;

	public GameObject loadInput;
	public string loadInputString;
	public GameObject writeInput;
	public string writeInputString;

	public Vector3 startPos;
	public Vector3 endPos;

	public GameObject player;

	public delegate void EventBlockChangedWithType(int blockType);
	public static event EventBlockChangedWithType OnEventBlockChanged;


	// Use this for initialization
	void Start () {

		currentLevel = Application.loadedLevelName;
		
		voxelGenerator = GetComponent<VoxelGenerator> ();
		terrainArray = new int[chunkSize, chunkSize, chunkSize];
		stoneNodes = new List<Vector3>();

		voxelGenerator.Initialize ();
		InitialiseTerrain();
		CreateTerrain();
		voxelGenerator.UpdateMesh ();

		if(gameObject.name == "VoxelObjectPrefab(Clone)"){

			string loadChunk1 = "AssessmentChunk1";
						
				terrainArray =
					XMLVoxelFileWriter.LoadChunkFromXMLFile (16, loadChunk1);
				CreateTerrain ();
				voxelGenerator.UpdateMesh ();

		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.F1)){

			Debug.Log("F1 Pressed");

			if(currentLevel == "Scene 1 Voxel Scene"){
			player.SetActive(false);
			}
			writeInput.SetActive(true);				
			}

		if (Input.GetKeyDown (KeyCode.F2)) {

			if(currentLevel == "Scene 1 Voxel Scene"){
			player.SetActive(false);
			}
			loadInput.SetActive(true);

		}
		
	}

	void OnEnable(){

		PlayerScript.OnEventChangeBlock +=
			SetBlock;

	}

	void OnDisable(){

		PlayerScript.OnEventChangeBlock -=
			SetBlock;

	}

	public void SaveLevel(string writeInputString){

		Debug.Log ("SaveLevel");
		Debug.Log (writeInputString);

		XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, writeInputString);

		writeInput.SetActive(false);

		if(currentLevel == "Scene 1 Voxel Scene"){
		player.SetActive(true);
		}

	}


	public void LoadChunk1(){
		
			Debug.Log("Loading Chunk1");

			terrainArray =
				XMLVoxelFileWriter.LoadChunkFromXMLFile (16, "AssessmentChunk1");

			CreateTerrain ();
			voxelGenerator.UpdateMesh ();

			Vector3 s, e;
			if(XMLVoxelFileWriter.ReadStartAndEndPosition(
				out s, out e, "AssessmentChunk1")){

				startPos = s;
				endPos = e;

				}
			}

	public void LoadChunk2(){
			
			Debug.Log("Loading Chunk1");
			
			terrainArray =
				XMLVoxelFileWriter.LoadChunkFromXMLFile (16, "AssessmentChunk2");

			CreateTerrain ();
			voxelGenerator.UpdateMesh ();
			
			Vector3 s, e;
			if(XMLVoxelFileWriter.ReadStartAndEndPosition(
				out s, out e, "AssessmentChunk2")){
				
				startPos = s;
				endPos = e;
				
			}
		}



	public void LoadLevel(string loadInputString){

		Debug.Log ("LoadLevel");
		Debug.Log (loadInputString);

		if (System.IO.File.Exists (loadInputString + ".xml")) {

			terrainArray =
			XMLVoxelFileWriter.LoadChunkFromXMLFile (16, loadInputString);
			CreateTerrain ();
			voxelGenerator.UpdateMesh ();

			loadInput.SetActive (false);

			if(currentLevel == "Scene 1 Voxel Scene"){
			player.SetActive (true);
			}

		} else {

			loadInputString = "VoxelChunk";

			terrainArray =
				XMLVoxelFileWriter.LoadChunkFromXMLFile (16, loadInputString);
			CreateTerrain ();
			voxelGenerator.UpdateMesh ();
			
			loadInput.SetActive (false);
			player.SetActive (true);
		}

	}
	

	public int GetChunkSize(){

		return chunkSize;

	}

	public int GetChunkSize(Vector3 vecIn){

		return chunkSize;

	}



//	public int IsTraversableCosted(Vector3 voxel)
//	{
//
//		// is block empty
//		bool isEmpty = terrainArray[(int)voxel.x,
//		                            (int)voxel.y, (int)voxel.z] == 0;
//		// is block below stone
//		bool isBelowStone = terrainArray[(int)voxel.x,
//		                                 (int)voxel.y - 1, (int)voxel.z] == 3;
//		// is block below dirt
//		bool isBelowDirt = terrainArray[(int)voxel.x,
//		                                 (int)voxel.y - 1, (int)voxel.z] == 2;
//
//		if(isEmpty && isBelowStone){
//
//			return 1;
//		}
//		if(isEmpty && isBelowDirt){
//			return 3;
//		}
//
//		return 0;
//	}

	public bool isTraversable(Vector3 voxel){

		// is block empty
		bool isEmpty = terrainArray[(int)voxel.x,
		                            (int)voxel.y, (int)voxel.z] == 0;
		// is block below stone
		bool isBelowStone = terrainArray[(int)voxel.x,
		                                 (int)voxel.y - 1, (int)voxel.z] == 3;
		// is block below dirt
		bool isBelowDirt = terrainArray[(int)voxel.x,
		                                (int)voxel.y - 1, (int)voxel.z] == 2;

		if(isEmpty && isBelowStone){
			return true;
		}
		if(isEmpty && isBelowDirt){
			return true;
		}
		return false;

	}
	
	void InitialiseTerrain(){
		
		terrainArray = new int[chunkSize, chunkSize, chunkSize];
		
		for (int x = 0; x < terrainArray.GetLength(0); x++) {
			
			for(int y = 0; y < terrainArray.GetLength(1); y++)
			{
				
				for(int z = 0; z < terrainArray.GetLength(2); z++){
					
					if(y == 3){
						
						terrainArray[x, y, z] = 1;
						
					}
					else if (y < 3){
						
						terrainArray[x, y, z] = 2;
						
					}
					if(terrainArray[x, y, z] == 3){

						stoneNodes.Add(new Vector3(x, y, z));
						Debug.Log(stoneNodes);

					}
					
				}
				
			}
			
		}

		terrainArray[0, 3, 1] = 3;
		terrainArray[0, 3, 2] = 3;
		terrainArray[0, 3, 3] = 3;
		terrainArray[1, 3, 3] = 3;
		terrainArray[1, 3, 4] = 3;
		terrainArray[2, 3, 4] = 3;
		terrainArray[3, 3, 4] = 3;
		terrainArray[4, 3, 4] = 3;
		terrainArray[5, 3, 4] = 3;
		terrainArray[5, 3, 3] = 3;
		terrainArray[5, 3, 2] = 3;
		terrainArray[6, 3, 2] = 3;
		terrainArray[7, 3, 2] = 3;
		terrainArray[8, 3, 2] = 3;
		terrainArray[9, 3, 2] = 3;
		terrainArray[10, 3, 2] = 3;
		terrainArray[11, 3, 2] = 3;
		terrainArray[12, 3, 2] = 3;
		terrainArray[13, 3, 2] = 3;
		terrainArray[13, 3, 3] = 3;
		terrainArray[14, 3, 3] = 3;
		terrainArray[15, 3, 3] = 3;
		
	}

	[RPC] public void SetBlock(Vector3 index, int blockType)
	{

		if ((index.x >= 0 &&
			index.x <= terrainArray.GetLength (0)) &&
			(index.y >= 0 &&
			index.y <= terrainArray.GetLength (1)) &&
			(index.z >= 0 && index.z <= terrainArray.GetLength (2))) { 

			if(blockType == 0 && currentLevel != "Scene 3 - Networking Scene"){

			Instantiate(blockPrefabs[terrainArray[(int)index.x, (int)index.y, (int)index.z] - 1], new Vector3 (index.x, index.y + 2, index.z), Quaternion.identity);
			}


			// Change the block to the required type
			terrainArray [(int)index.x, (int)index.y, (int)index.z] =
			blockType;
			// Create the new mesh
			CreateTerrain ();
			// Update the mesh data
			voxelGenerator.UpdateMesh ();

			if(currentLevel != "Scene 3 - Networking Scene"){

			OnEventBlockChanged(blockType);

			}

			Debug.Log (blockType);

		}
	}
	
	void CreateTerrain()
	{
		
		for (int x = 0; x < terrainArray.GetLength(0); x++) {
			
			for(int y = 0; y < terrainArray.GetLength(1); y++){
				
				for(int z = 0; z < terrainArray.GetLength(2); z++){
					
					if(terrainArray[x, y, z] != 0){
						
						string tex;
						switch (terrainArray[x, y, z]){
							
						case 1:
							tex = "Grass";
							break;
							
						case 2:
							tex = "Dirt";
							break;

						case 3:
							tex = "Stone";
							break;

						case 4:
							tex = "Sand";
							break;
							
						default:
							tex = "Grass";
							break;
							
						}
						// check if we need to draw the negative x face
						if (x == 0 || terrainArray[x - 1, y, z] == 0)
						{
							voxelGenerator.CreateNegativeXFace(x, y, z, tex);
						}
						// check if we need to draw the positive x face
						if (x == terrainArray.GetLength(0) - 1 ||
						    terrainArray[x + 1, y, z] == 0)
						{
							voxelGenerator.CreatePositiveXFace(x, y, z, tex);
						} 
						// check if we need to draw the negative y face
						if (y == 0 || terrainArray[x, y - 1, z] == 0)
						{
							voxelGenerator.CreateNegativeYFace(x, y, z, tex);
						}
						// check if we need to draw the positive y face
						if (y == terrainArray.GetLength(1) - 1 ||
						    terrainArray[x, y + 1, z] == 0)
						{
							voxelGenerator.CreatePositiveYFace(x, y, z, tex);
						} 
						// check if we need to draw the negative y face
						if (z == 0 || terrainArray[x, y, z - 1] == 0)
						{
							voxelGenerator.CreateNegativeZFace(x, y, z, tex);
						}
						// check if we need to draw the positive y face
						if (z == terrainArray.GetLength(2) - 1 ||
						    terrainArray[x, y, z + 1] == 0)
						{
							voxelGenerator.CreatePositiveZFace(x, y, z, tex);
						} 

						
					}
					
				}
				
			}
			
		}
		
	}

}