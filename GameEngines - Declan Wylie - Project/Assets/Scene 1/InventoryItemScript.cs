using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItemScript : MonoBehaviour {

	// public variables to access UI components
	public Image itemSprite;
	public Text itemNameText;
	public Text itemAmountText;
	// public variables to sort on
	public string itemName;
	public int itemAmount;



	public static bool mergeSortDelegateAsc(InventoryItemScript item1, InventoryItemScript item2){

		return item1.itemAmount < item2.itemAmount;

	}
	public static bool mergeSortDelegateDesc(InventoryItemScript item1, InventoryItemScript item2){

		return item1.itemAmount > item2.itemAmount;

	}
	public static bool mergeSortDelegateAtoZ(InventoryItemScript item1, InventoryItemScript item2){

		return string.Compare(item1.itemName, item2.itemName) < 0;

	}
	public static bool mergeSortDelegateZtoA(InventoryItemScript item1, InventoryItemScript item2){

		return string.Compare(item1.itemName, item2.itemName) > 0;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
