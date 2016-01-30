using UnityEngine;
using System.Collections;

public enum PlayerType {WIZARD, RAT, CAT, ELEPHANT};

public class PlayerController : MonoBehaviour {

	public PlayerType Type { get; private set; }
	public int Score {get; set;}
	public int Number{ get; private set; }

	const float MAXSPEED = 3.5f;
	bool facingLeft = true;


	// Use this for initialization
	void Start () {


	}

	void FixedUpdate()
	{
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");

		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * MAXSPEED, moveY * MAXSPEED);

		if (moveX < 0 && !facingLeft)
			Flip ();
		else if (moveX > 0 && facingLeft)
			Flip ();

		Debug.Log (moveX);
	}

	void Flip()
	{
		facingLeft = !facingLeft;
		GetComponent<SpriteRenderer> ().flipX = !facingLeft;
	}

	public void Transform(PlayerType transform)
	{
		Type = transform;
	}
}
