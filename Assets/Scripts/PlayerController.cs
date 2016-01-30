using UnityEngine;
using System.Collections;

public enum PlayerType {WIZARD, MOUSE, CAT, ELEPHANT};

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
        Type = PlayerType.WIZARD;
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


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Item>() != null) {
            collidingItem = collider.gameObject;
        }
        else if (collider.name == "Cauldron") {
            isNearCauldron = true;
        }
    }


    void Win(PlayerController otherPlayer)
    {
        Destroy(otherPlayer.gameObject);
    }


    void Lose(PlayerController otherPlayer)
    {
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController otherPlayer = collision.gameObject.GetComponent<PlayerController>();

         if (otherPlayer != null) { //Colliding with another player
            if (Type == PlayerType.WIZARD) {
                if (otherPlayer.IsTransformed()) {
                    Lose(otherPlayer);
                }
            }
            else if (!otherPlayer.IsTransformed()) {
                Win(otherPlayer);
            }
            else if (Type == PlayerType.MOUSE) {
                if (otherPlayer.Type == PlayerType.ELEPHANT) {
                    Win(otherPlayer);
                }
            }
            else if (Type == PlayerType.CAT) {
                if (otherPlayer.Type == PlayerType.MOUSE) {
                    Win(otherPlayer);
                }
            }
            else if (Type == PlayerType.ELEPHANT) {
                if (otherPlayer.Type == PlayerType.CAT) {
                    Win(otherPlayer);
                }
            }
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

        if (isNearCauldron && pickedUpItemType != ItemType.NONE && Type == PlayerType.WIZARD) {
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

        Invoke("OnTransformTimeUp", Constants.TRANSFORMATION_DURATION);
        pickedUpItemType = ItemType.NONE;
    }


    void OnTransformTimeUp()
    {
        SetColor(new Color(1, 1, 1));
        Type = PlayerType.WIZARD;
    }


    void TransformIntoMouse()
    {
        SetColor(new Color(0, 0, 2));
        Type = PlayerType.MOUSE;
    }


    void TransformIntoCat()
    {
        SetColor(new Color(0, 2, 0));
        Type = PlayerType.CAT;
    }


    void TransformIntoElephant()
    {
        SetColor(new Color(2, 0, 0));
        Type = PlayerType.ELEPHANT;
    }


    void SetColor(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().material.color = color;
    }


    public bool IsTransformed()
    {
        return Type != PlayerType.WIZARD;
    }
}
