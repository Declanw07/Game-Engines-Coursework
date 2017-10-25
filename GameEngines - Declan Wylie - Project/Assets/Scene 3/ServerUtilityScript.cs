using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ServerUtilityScript : MonoBehaviour {

	string typeName = "WGEDeclanGame";
	string gameName = "WGEDeclanRoom";
	HostData[] hostList;

	int buttonCount;


	public GameObject startServerButton;
	public GameObject ipInputField;
	public GameObject inactiveStartButton;
	public GameObject voxelChunkPrefab;


	public GameObject networkFPCPrefab;
	public GameObject mainCamera;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(Network.isClient || Network.isServer){

			if(startServerButton != null){
			startServerButton.SetActive(false);
			}
			if(ipInputField != null){
			ipInputField.SetActive(false);
			}

		}
	
	}

	public void StartServer()
	{
		if (!Network.isServer && !Network.isClient)
		{
			Network.InitializeServer(4, 25000,
			                         !Network.HavePublicAddress());
			MasterServer.RegisterHost(typeName, gameName);
		}
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList = MasterServer.PollHostList();
		}
	}


	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		Network.Instantiate(voxelChunkPrefab, Vector3.zero,
		                    Quaternion.identity, 0);

	}

	public void SetIP(string ipAddress){

		MasterServer.ipAddress = ipAddress;

		ipInputField.SetActive(false);

		MasterServer.RequestHostList(typeName);

	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");

		Network.Instantiate (networkFPCPrefab,
		                    new Vector3 (8, 8, 8), Quaternion.identity, 0);

	}

	void OnGUI(){

		if(hostList != null && !Network.isClient && !Network.isServer){

			for(int i = 0; i < hostList.Length; i++){

				if(hostList[i].gameName == gameName){
					if(GUI.Button(new Rect(10, 75 * i, 100, 50), "Join")){

						Network.Connect(hostList[i]);
					}
				}
			}
		}
	}
}
