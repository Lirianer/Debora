using UnityEngine;
using System.Collections.Generic;
using System;


public class ItemManager : MonoBehaviour
{
    GameObject fishPrefab;
    GameObject peanutPrefab;
    GameObject cheesePrefab;

    List<GameObject> itemPrefabs;

    void Start()
    {
        fishPrefab = Resources.Load("Fish") as GameObject;
        peanutPrefab = Resources.Load("Peanut") as GameObject;
        cheesePrefab = Resources.Load("Cheese") as GameObject;

        itemPrefabs = new List<GameObject> { fishPrefab, peanutPrefab, cheesePrefab };

        InvokeRepeating("OnSpawnItemTime", 0, Constants.TIME_BETWEEN_ITEM_SPAWNS);
    }


    void OnSpawnItemTime()
    {
        print(GetRandomSpawnPos());

        GameObject randomItemPrefab = Utils.mGetRandomListElement<GameObject>(itemPrefabs);
        Instantiate(randomItemPrefab, GetRandomSpawnPos(), Quaternion.identity);

        print("instantiate " + randomItemPrefab);
    }


    Vector2 GetRandomSpawnPos()
    {
        List<Transform> allFloorTransforms = GetAllFloorTransforms();

        Transform randomFloorTransform;
        do {
            randomFloorTransform = Utils.mGetRandomListElement<Transform>(allFloorTransforms);
        }
        while (IsThereAColliderNearby(randomFloorTransform));

        return randomFloorTransform.position;
    }


    bool IsThereAColliderNearby(Transform transform)
    {
        List<Collider2D> colliders = new List<Collider2D>(FindObjectsOfType<Collider2D>());
        return colliders.Exists(x => IsColliderNear(x, transform));
    }


    bool IsColliderNear(Collider2D collider, Transform transform)
    {
        return Vector2.Distance(collider.transform.position, transform.position) < 0.5f;
    }
    

    List<Transform> GetAllFloorTransforms()
    {
        return new List<Transform>(GameObject.Find("Floors").GetComponentsInChildren<Transform>());
    }


    List<Transform> GetAllWallTransforms()
    {
        return new List<Transform>(GameObject.Find("Floors").GetComponentsInChildren<Transform>());
    }
}
