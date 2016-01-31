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

        SpawnItem();
        InvokeRepeating("SpawnItem", 0, Constants.TIME_BETWEEN_ITEM_SPAWNS);
    }


    void SpawnItem()
    {
        GameObject randomItemPrefab = Utils.mGetRandomListElement<GameObject>(itemPrefabs);
        Instantiate(randomItemPrefab, GetRandomSpawnPos(), Quaternion.identity);
    }


    static Vector2 GetRandomSpawnPos()
    {
        return GetRandomPositionAwayFromWallsAndCauldron();
    }


    static bool IsNearCauldron(Vector2 pos)
    {
        return Vector2.Distance(pos, GetCauldronPos()) < 4;
    }

    
    static Vector2 GetCauldronPos()
    {
        return GameObject.Find("Cauldron").transform.position;
    }


    static public Vector2 GetRandomPositionAwayFromWallsAndCauldron()
    {
        Vector2 pos;
        do {
            pos = GetRandomPositionAwayFromWalls();
        }
        while (IsNearCauldron(pos));

        return pos;
    }


    static public Vector2 GetRandomPositionAwayFromWalls()
    {
        List<GameObject> allFloorTransforms = GetAllFloorGameObjects();

        GameObject randomFloorGO;
        do {
            randomFloorGO = Utils.mGetRandomListElement<GameObject>(allFloorTransforms);
        }
        while (IsThereAColliderNearby(randomFloorGO));

        return randomFloorGO.transform.position;
    }


    static bool IsThereAColliderNearby(GameObject go)
    {
        List<Collider2D> colliders = new List<Collider2D>(FindObjectsOfType<Collider2D>());
        return colliders.Exists(x => IsColliderNear(x, go.transform));
    }


    static bool IsColliderNear(Collider2D collider, Transform transform)
    {
        return Vector2.Distance(collider.transform.position, transform.position) < 1.3f;
    }
    

    static List<GameObject> GetAllFloorGameObjects()
    {
        //return new List<Transform>(GameObject.Find("Floors").GetComponentsInChildren<Transform>());

        return new List<GameObject>(GameObject.FindGameObjectsWithTag("Floor"));
    }



}
