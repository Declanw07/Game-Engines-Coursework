using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {

	public VoxelChunk voxelChunk;
	GameObject cube;
	bool traversing = false;

	public Vector3 startPosition = new Vector3(8, 4, 14);
	public Vector3 endPosition = new Vector3(11, 4, 2);
	Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		startPosition = voxelChunk.startPos;
		endPosition = voxelChunk.endPos;

	}

	public void startDijkskra(){

		if (!traversing) {
			
			Stack<Vector3> path = Dijkstra(
				startPosition, endPosition, voxelChunk);
			
			Debug.Log(path.Count);
			
			if(path.Count > 0){
				
				Debug.Log("Lerp Started");
				StartCoroutine(LerpAlongPath (path));
				
			}
		}


	}

	public void startDepthFirstSearch(){

		if (!traversing) {

		Stack<Vector3> path = DepthFirstSearch(
			startPosition, endPosition, voxelChunk);
		
		Debug.Log(path.Count);
		
		if(path.Count > 0){
			
			Debug.Log("Lerp Started");
			StartCoroutine(LerpAlongPath (path));
			
		}
		}

	}

	public void startBreadthFirstSearch(){

		if (!traversing) {

			Debug.Log(startPosition);
			Debug.Log(endPosition);

		Stack<Vector3> path = BreadthFirstSearch(
			startPosition, endPosition, voxelChunk);

			foreach(Vector3 p in path){
				Debug.Log(p);
			}
		
		Debug.Log (path.Count);
		
		if(path.Count > 0){
			
			StartCoroutine(LerpAlongPath (path));
		}

		}

	}

	IEnumerator LerpAlongPath(Stack<Vector3> path){

		Debug.Log("Lerping Started");

		traversing = true;
		float lerpTime = 1.0f;

		if (cube != null) {

			DestroyObject(cube);

		}

		cube = GameObject.CreatePrimitive (PrimitiveType.Cube);

		Vector3 current = path.Pop ();
		cube.transform.position = current;

		while (path.Count > 0) {

			Vector3 target = path.Pop ();
			float currentTime = 0.0f;

			while (currentTime < lerpTime){

				currentTime += Time.deltaTime;
				cube.transform.position = Vector3.Lerp(
					current, target, currentTime / lerpTime);

				yield return 0;
			}
			cube.transform.position = target;
			current = target;

			}

		traversing = false;

	}


	// CURRENTLY BROKEN, Gives Keynotfoundexception on Assessment Chunk 2, Stack for start and end positions are also off, Also gives index out of range exception on the beginning scene (VoxelChunk)
	Stack<Vector3> Dijkstra(
		Vector3 start, Vector3 end, VoxelChunk vc){

		Debug.Log("Dijkstra's Algorithm Started");

		Dictionary<Vector3, int> distance = new Dictionary<Vector3, int>();
		Dictionary<Vector3, Vector3> parent = new Dictionary<Vector3, Vector3>();

		Stack<Vector3> waypoints = new Stack<Vector3>();

		distance[start] = 0;

		bool found = false;
		Vector3 current = start;

		List<Vector3> emptyList = new List<Vector3>();


		// Getting each node in graph and checking if traversable, if it is add it to emptyList.
		for(int x = 0; x < vc.GetChunkSize(); x++){
			for(int z = 0; z < vc.GetChunkSize(); z++){
				if(vc.isTraversable(new Vector3(x, 4, z)))
				{
					emptyList.Add(new Vector3(x, 4, z));
				}
			}
		}
		// While emptyList isn't empty and target isn't found.
		while(emptyList.Count > 0 && !found){
			// Get each node in emptyList.
			foreach(Vector3 min in emptyList)
			{
				// Check dictionary d for node, if node isn't null make the current
				if(!distance.ContainsKey(current))
				{
					current = min;
				}
				if(distance.ContainsKey(min))
				{
					if(distance[min] <= distance[current])
					{
						current = min;
					}
				}
			}
			emptyList.Remove(current);
			if(current != end)
			{
				List<Vector3> neighbourList = new List<Vector3>();

				neighbourList.Add (current + new Vector3(1, 0, 0));
				neighbourList.Add (current + new Vector3(-1, 0, 0));
				neighbourList.Add (current + new Vector3(0, 0, 1));
				neighbourList.Add (current + new Vector3(0, 0, -1));

				foreach(Vector3 n in neighbourList){

					if(vc.isTraversable(n)){

						int length = 1;
						int newDist = distance[current] + length;
						if(!distance.ContainsKey(n) || newDist < distance[n]){
							distance[n] = newDist;
							parent[n] = current;
						}

						}
					}
			}else
			{
				found = true;
			}
			current = emptyList[0];



		}

		if (found)
		{
			while (current != start)
			{
				waypoints.Push(current + offset);
				current = parent[current];				// Get keynotfoundexception here on Assessment Chunk 2.
			}
			waypoints.Push(start + offset);
		}

		return waypoints;
	}



	Stack<Vector3> BreadthFirstSearch(
		Vector3 start, Vector3 end, VoxelChunk vc){

		Stack<Vector3> waypoints = new Stack<Vector3>();

		Dictionary<Vector3, Vector3> visitedParent =
			new Dictionary<Vector3, Vector3>();
		Queue<Vector3> q = new Queue<Vector3>();
		bool found = false;
		Vector3 current = start;

		q.Enqueue(start);

		while (q.Count > 0 && !found){

			current = q.Dequeue();
			if(current != end)
			{
				// our adjacent nodes are x+1, x-1, z+1 and z-1
				List<Vector3> neighbourList = new List<Vector3>();
				neighbourList.Add(current + new Vector3(1, 0, 0)); // x+1
				neighbourList.Add(current + new Vector3(-1, 0, 0)); // x-1
				neighbourList.Add(current + new Vector3(0, 0, 1)); // z+1
				neighbourList.Add(current + new Vector3(0, 0, -1)); // z-1
				foreach (Vector3 n in neighbourList)
				{
					// check if n is within the terrain array range
					if ((n.x >= 0 && n.x < vc.GetChunkSize())
					    && n.z >= 0 && n.z < vc.GetChunkSize())
					{
						if(vc.isTraversable(n))
						{
							// check if node is already processed
							if (!visitedParent.ContainsKey(n))
							{
								visitedParent[n] = current;
								q.Enqueue(n);
							}
						}
					}
				}
			}
			else
			{

				found = true;

			}

		}
		// solution was found, so we can build a path of waypoints
		if (found)
		{
			while (current != start)
			{
				waypoints.Push(current + offset);
				current = visitedParent[current];
			}
			waypoints.Push(start + offset);
		}
		return waypoints;
	}

	Stack<Vector3> DepthFirstSearch(
		Vector3 start, Vector3 end, VoxelChunk vc){

		Stack<Vector3> waypoints = new Stack<Vector3> ();

		Dictionary<Vector3, Vector3> visitedParent = 
			new Dictionary<Vector3, Vector3> ();
		Stack<Vector3> theStack = new Stack<Vector3> ();
		bool found = false;
		Vector3 current = start;

		theStack.Push (start);

		while (theStack.Count > 0 && !found) {

			current = theStack.Pop();
			if(current != end){

				List<Vector3> neighbourList = new List<Vector3>();
				neighbourList.Add(current + new Vector3(1, 0, 0));
				neighbourList.Add(current + new Vector3(-1, 0, 0));
				neighbourList.Add(current + new Vector3(0, 0, 1));
				neighbourList.Add(current + new Vector3(0, 0, -1));

				foreach(Vector3 n in neighbourList){

					if((n.x >= 0 && n.x < vc.GetChunkSize()
					    && n.z >= 0 && n.z < vc.GetChunkSize())){

						if(vc.isTraversable(n))
						{
							if(!visitedParent.ContainsKey(n)){

								visitedParent[n] = current;
								theStack.Push(n);

							}

						}

					}

				}

			}else{

				found = true;
		}

	}

	if(found){

		while (current != start)
		{
			waypoints.Push(current + offset);
			current = visitedParent[current];
		}
		waypoints.Push(start + offset);
		}
		
		return waypoints;

	}

}
