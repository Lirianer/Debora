using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    Dictionary<ItemType, GameObject> itemIcons;

	
	void Awake ()
    {
        itemIcons = new Dictionary<ItemType, GameObject>();
        itemIcons[ItemType.CHEESE] = transform.Find("CheeseIcon").gameObject;
        itemIcons[ItemType.FISH] = transform.Find("FishIcon").gameObject;
        itemIcons[ItemType.PEANUT] = transform.Find("PeanutIcon").gameObject;

        HideIcon();
    }
	
	
	public void ShowIcon(ItemType itemType)
    {
        foreach (ItemType type in itemIcons.Keys) {
            //Show the item I want and hide the others
            if (type == itemType) {
                itemIcons[type].SetActive(true);
            }
            else {
                itemIcons[type].SetActive(false);
            }
        }
    }


    public void HideIcon()
    {
        foreach (GameObject icon in itemIcons.Values) {
            icon.SetActive(false);
        }
    }
}
