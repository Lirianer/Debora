using UnityEngine;
using System.Collections;

public enum PlayerType {WIZARD, RAT, CAT, ELEPHANT};

public class PlayerController : MonoBehaviour {

    public PlayerType Type { get; private set; }
    public int Score { get; set; }
    public int number;

    bool facingLeft = true;
    PlayerInput input;
    ItemType pickedUpItemType = ItemType.NONE;
    bool isNearCauldron = false;

    GameObject collidingItem = null;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }


    void FixedUpdate()
    {
        float moveX = input.GetHorizontalAxis();
        float moveY = input.GetVerticalAxis();

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * Constants.PLAYER_NORMAL_SPEED, moveY * Constants.PLAYER_NORMAL_SPEED);

        if (moveX < 0 && !facingLeft)
            Flip();
        else if (moveX > 0 && facingLeft)
            Flip();

    }

    void Flip()
    {
        facingLeft = !facingLeft;
        GetComponent<SpriteRenderer>().flipX = !facingLeft;
    }

    public void Transform(PlayerType transform)
    {
        Type = transform;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>() != null) {
            collidingItem = collision.gameObject;
        }
        else if (collision.name == "Cauldron") {
            isNearCauldron = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        collidingItem = null;
        if (collision.name == "Cauldron") {
            isNearCauldron = false;
        }
    }
    

    public void OnActionButton()
    {
        if (collidingItem != null && pickedUpItemType == ItemType.NONE) {
            pickedUpItemType = collidingItem.GetComponent<Item>().GetItemType();
            Destroy(collidingItem.gameObject);
        }

        if (isNearCauldron && pickedUpItemType != ItemType.NONE) {
            Transform();
        }
    }


    void Transform()
    {
        if (pickedUpItemType == ItemType.CHEESE) {
            TransformIntoMouse();
        }
        else if (pickedUpItemType == ItemType.FISH) {
            TransformIntoCat();
        }
        else if (pickedUpItemType == ItemType.PEANUT) {
            TransformIntoElephant();
        }
    }


    void TransformIntoMouse()
    {
        SetColor(new Color(0, 0, 2));
    }


    void TransformIntoCat()
    {
        SetColor(new Color(0, 2, 0));
    }


    void TransformIntoElephant()
    {
        SetColor(new Color(2, 0, 0));
    }


    void SetColor(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().material.color = color;
    }
}
