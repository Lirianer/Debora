using UnityEngine;
using System.Collections;

public class Cauldron : MonoBehaviour {

    void Start()
    {
        //InvokeRepeating("Relocate", Constants.CAULDRON_RELOCATE_TIME, Constants.CAULDRON_RELOCATE_TIME);
    }


	public void Relocate()
    {
        transform.position = ItemManager.GetRandomPositionAwayFromWalls();
    }
}
