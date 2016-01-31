using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{

    public static BoxManager instance;

    GameObject boxPrefab;

	void Start ()
    {
        instance = this;
        boxPrefab = Resources.Load("Box") as GameObject;
        SpawnBoxes();
	}


    void SpawnBoxes()
    {
        for (int i=0; i<Constants.NUM_BOXES; i++) {
            Vector2 pos = ItemManager.GetRandomPositionAwayFromWallsAndCauldron();
            GameObject box = Instantiate(boxPrefab, pos, Quaternion.identity) as GameObject;
            box.GetComponent<Rigidbody2D>().mass = Utils.mGetRandomIntBetween(1, 100);
        }
    }


    public void RespawnBoxes()
    {
        List<GameObject> boxes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Box"));
        boxes.ForEach(x => Destroy(x));
        SpawnBoxes();
    }
}
