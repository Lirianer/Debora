using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemManager {

	Dictionary<Item, GameObject> itemGoDictionary;
	Action OnSpawnItem;

	public ItemManager()
	{
		itemGoDictionary = new Dictionary<Item, GameObject>();
	}

	public void SpawnRandomItem(GameObject go)
	{
		ItemType type = (ItemType)UnityEngine.Random.Range(0, (float)Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Max());
		SpawnItem (type, go);
	}

	public void SpawnItem (ItemType type, GameObject go)
	{
		
	}

	public void RegisterOnSpawnItem(Action value)
	{
		OnSpawnItem = value;
	}
}
