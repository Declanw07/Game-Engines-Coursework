using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

	// Parent object inventory item
	public Transform parentPanel;
	// Item info to build inventory items
	public List<Sprite> itemSprites;
	public List<string> itemNames;
	public List<int> itemAmounts;
	// Starting template item
	public GameObject startItem;

	public List<InventoryItemScript> inventoryList;
	public List<int> testNums;

	public delegate bool mergeSortDelegate(InventoryItemScript item1, InventoryItemScript item2);
		
	// Use this for initialization
	void Start () {
	
			inventoryList = new List<InventoryItemScript>();
			for(int i = 0; i < itemNames.Count; i++)
			{
				// Create a duplicate of the starter item
				GameObject inventoryItem =
					(GameObject)Instantiate(startItem);
				// UI items need to parented by the canvas or an object within the canvas
					inventoryItem.transform.SetParent(parentPanel);
				// Original start item is disabled – so the duplicate must be enabled
					inventoryItem.SetActive(true);
				// Get InventoryItemScript component so we can set the data
					InventoryItemScript iis =
						inventoryItem.GetComponent<InventoryItemScript>();
				iis.itemSprite.sprite = itemSprites[i];
				iis.itemNameText.text = itemNames[i];
				iis.itemName = itemNames[i];
				iis.itemAmountText.text = itemAmounts[i].ToString();
				iis.itemAmount = itemAmounts[i];
				// Keep a list of the inventory items
				inventoryList.Add(iis);
		}

		DisplayListInOrder();

	}
	
	// Update is called once per frame
	void Update () {


	}

	void DisplayListInOrder()
	{
		// Height of item plus space between each
		float xOffset = -95f;
		// Use the start position for the first item
		Vector3 startPosition = startItem.transform.position;
		foreach(InventoryItemScript iis in inventoryList)
		{
			iis.transform.position = startPosition;
			//set position of next item using offset
			startPosition.x -= xOffset;
		}
	}

	public void StartMergeSortAsc(){
		// Sort inventoryList passing in inventoryList to the GetMergeSort method along with the appropriate Delegate to for this method.
		inventoryList = GetMergeSort(inventoryList, InventoryItemScript.mergeSortDelegateAsc);
		DisplayListInOrder();

	}
	public void StartMergeSortDesc(){
		// Sort inventoryList passing in inventoryList to the GetMergeSort method along with the appropriate Delegate to for this method.
		inventoryList = GetMergeSort(inventoryList, InventoryItemScript.mergeSortDelegateDesc);
		DisplayListInOrder();
		
	}
	public void StartMergeSortAtoZ(){
		// Sort inventoryList passing in inventoryList to the GetMergeSort method along with the appropriate Delegate to for this method.
		inventoryList = GetMergeSort(inventoryList, InventoryItemScript.mergeSortDelegateAtoZ);
		DisplayListInOrder();
		
	}
	public void StartMergeSortZtoA(){
		// Sort inventoryList passing in inventoryList to the GetMergeSort method along with the appropriate Delegate to for this method.
		inventoryList = GetMergeSort(inventoryList, InventoryItemScript.mergeSortDelegateZtoA);
		DisplayListInOrder();
		
	}

	void UpdateInventory(int[] blockAmount){

		Debug.Log("Updating Inventory");

		for(int i = 0; i < inventoryList.Count; i++){
		
			inventoryList[i].itemAmountText.text = blockAmount[i].ToString();
			inventoryList[i].itemAmount = blockAmount[i];
			itemAmounts[i] = blockAmount[i];

		}

	}

	void OnEnable(){
		PlayerScript.OnEventUpdateInventory+=
			UpdateInventory;

	}

	void OnDisable(){
		PlayerScript.OnEventUpdateInventory-=
			UpdateInventory;

	}

	List<InventoryItemScript> GetMergeSort(List<InventoryItemScript> listIn, mergeSortDelegate mD){
		
		if(listIn.Count <= 1){
			return listIn;
		}
		
		List<InventoryItemScript>leftList = new List<InventoryItemScript>();
		List<InventoryItemScript>rightList = new List<InventoryItemScript>();

		for(int i = 0; i < listIn.Count; i++){


			
			if(i < listIn.Count/2){
				leftList.Add(listIn[i]);
			}
			else{
				rightList.Add(listIn[i]);
			}
		}

		leftList = GetMergeSort(leftList, mD);
		rightList = GetMergeSort(rightList, mD);

		listIn = MergeSort(leftList, rightList, mD);

		return listIn;
		
	}
	
	
	List<InventoryItemScript> MergeSort(List<InventoryItemScript> lList, List<InventoryItemScript> rList, mergeSortDelegate mD){


		
		List<InventoryItemScript> mergedList = new List<InventoryItemScript>();
		
		int i = 0;
		int j = 0;

		Debug.Log(lList.Count);
		Debug.Log(rList.Count);
		
		while(i < lList.Count && j < rList.Count){

			if(mD(lList[i], rList[j])){

				mergedList.Add(lList[i]);
				i++;

			}else{

				mergedList.Add(rList[j]);
				j++;

			}
		}
		if(i < lList.Count){

			lList.Count.ToString();

			mergedList.AddRange(lList.GetRange(i, lList.Count-i));

		}else{

			mergedList.AddRange(rList.GetRange(j, rList.Count-j));

		}

		return mergedList;
	}
}
