using UnityEngine;
using System.Collections;

public enum ItemType {PEANUT, FISH, CHEESE}

public class Item {
	public ItemType Type { get; private set; }
	Sprite sprite;

	public Item (ItemType type)
	{
		Type = type;

		switch (Type) {
		case ItemType.PEANUT:
			break;
		case ItemType.FISH:
			break;
		case ItemType.CHEESE:
			break;
		}


	}
}
