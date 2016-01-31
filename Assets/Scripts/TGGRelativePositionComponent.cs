using UnityEngine;
using System.Collections;

public class TGGRelativePositionComponent : GameBehaviour 
{	
	public enum XDistanceType {FROM_LEFT, FROM_RIGHT};

	public XDistanceType xDistanceType;
	public float viewportXDistance;
	public float viewportYDistance;
	public bool ignoreY;
	public bool printViewportPos;

	void Update () 
	{
		#if UNITY_EDITOR
		SetPosition();
		#endif
	}


	void SetPosition ()
	{
		if (printViewportPos) {
			Debug.Log (Camera.main.WorldToViewportPoint (transform.position));
			return;
		}
		float worldY;
		if (ignoreY) {
			worldY = transform.position.y;
		}
		else {
			worldY = ViewportToWorldY (viewportYDistance);
		}
		float worldX;
		if (xDistanceType == XDistanceType.FROM_LEFT) {
			worldX = ViewportToWorldX (viewportXDistance);
		}
		else
			if (xDistanceType == XDistanceType.FROM_RIGHT) {
				worldX = ViewportToWorldX (1 - viewportXDistance);
			}
			else {
				throw new UnityException ();
			}
		transform.position = new Vector3 (worldX, worldY, transform.position.z);
	}


	void Start()
	{
		SetPosition();
	}

}
