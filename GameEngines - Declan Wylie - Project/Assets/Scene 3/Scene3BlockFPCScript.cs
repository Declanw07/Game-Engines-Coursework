using UnityEngine;
using System.Collections;

public class Scene3BlockFPCScript : MonoBehaviour {

	//float lastSyncTime = 0f;
	//float syncDelay = 0f;
	//float syncTime = 0f;
	//Vector3 startPosition = Vector3.zero;
	//Vector3 endPosition = Vector3.zero;

	//// Use this for initialization
	//void Start () {
	
	//	if(networkView.isMine){
			
	//		MonoBehaviour[] components =
	//			GetComponents<MonoBehaviour>();
	//		foreach(MonoBehaviour m in components){
	//			m.enabled = true;
	//		}
	//		foreach(Transform t in transform){
	//			t.gameObject.SetActive(true);
	//		}
			
	//	}

	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//	if (!networkView.isMine)
	//	{
	//		syncTime += Time.deltaTime;
	//		if (syncTime < syncDelay)
	//		{
	//			transform.position = Vector3.Lerp(startPosition,
	//			                                  endPosition, syncTime / syncDelay);
	//		}
	//	}
	//	else
	//	{
	//		if (Input.GetButtonDown("Fire1"))
	//		{

	//			Debug.Log("Fire 1 pressed.");
	//			Vector3 v;
	//			VoxelChunk vcs;
	//			if (PickThisBlock(out v, out vcs, 4))
	//			{

	//				NetworkView nv =
	//					vcs.GetComponent<NetworkView>();
	//				if (nv != null)
	//				{

	//					Debug.Log("nv isn't null");
	//					nv.RPC("SetBlock", RPCMode.All, v, 0);
	//				}
	//			}
	//		}
	//	} 

	//}

	//void OnSerializeNetworkView(BitStream stream,
	//                            NetworkMessageInfo info)
	//{
	//	Vector3 syncPosition = Vector3.zero;
	//	Vector3 syncVelocity = Vector3.zero;
	//	if (stream.isWriting)
	//	{
	//		syncPosition = rigidbody.position;
	//		stream.Serialize(ref syncPosition);
	//		syncVelocity = rigidbody.velocity;
	//		stream.Serialize(ref syncVelocity);
	//	}
	//	else
	//	{
	//		stream.Serialize(ref syncPosition);
	//		syncTime = 0f;
	//		syncDelay = Time.time - lastSyncTime;
	//		lastSyncTime = Time.time;
	//		startPosition = rigidbody.position;
	//		endPosition = syncPosition + syncVelocity * syncDelay;
	//	}
	//}

	//bool PickThisBlock(out Vector3 v, out VoxelChunk
	//                   voxelChunkScript, float dist)
	//{
	//	v = new Vector3();
	//	voxelChunkScript = null;
	//	Ray ray = new Ray(gameObject.transform.position, Vector3.down);

	//	Debug.Log(ray.origin);
	//	Debug.Log(ray.direction);

	//	RaycastHit hit;
	//	if (Physics.Raycast(ray, out hit, dist))
	//	{
	//		// check if the target we hit has a VoxelChunk script
	//		voxelChunkScript =
	//			hit.collider.gameObject.GetComponent<VoxelChunk>();
	//		if (voxelChunkScript != null)
	//		{
	//			// offset toward centre of the block hit
	//			v = hit.point - hit.normal / 2;
	//			// round down to get the index of the block hit
	//			v.x = Mathf.Floor(v.x);
	//			v.y = Mathf.Floor(v.y);
	//			v.z = Mathf.Floor(v.z);
	//			return true;
	//		}
	//	}
	//	return false;
	//}

}
