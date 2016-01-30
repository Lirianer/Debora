using UnityEngine;
using System.Collections;

public enum PlayerType {WIZARD, RAT, CAT, ELEPHANT};

public class PlayerController : MonoBehaviour {

	public PlayerType Type { get; private set; }
	public int Score {get; set;}
    public int number;

	const float MAXSPEED = 3.5f;
	bool facingLeft = true;
	PlayerInput input;


	// Use this for initialization
	void Start () {
		input = GetComponent<PlayerInput>();
	}

	void FixedUpdate()
	{
		//float moveX = Input.GetAxis ("Horizontal");
		//float moveY = Input.GetAxis ("Vertical");
		
		float moveX = input.GetHorizontalAxis();
		float moveY = input.GetVerticalAxis();

		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * MAXSPEED, moveY * MAXSPEED);

		if (moveX < 0 && !facingLeft)
			Flip ();
		else if (moveX > 0 && facingLeft)
			Flip ();

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
