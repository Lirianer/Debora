using UnityEngine;
using System.Collections;

public enum ItemType {PEANUT, FISH, CHEESE, NONE}

public class Item : MonoBehaviour
{
    public ItemType type;
    

    void Start()
    {
        Invoke("OnTimeUp", Constants.ITEM_DURATION);
    }


    void OnTimeUp()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


    public ItemType GetItemType()
    {
        return type;
    }
}
