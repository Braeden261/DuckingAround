using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    public float playerSpeed;
    private bool isHoldingDuck;
    private bool canPickup;
    public Transform holdPoint;
    private GameObject grabbableDuck = null;
    private GameObject heldDuck = null;

    //adding custom controls for each player
    public KeyCode left;
    public KeyCode right;
    public KeyCode action;

    private Rigidbody2D RB;

    float startTime = 0.0f;
    float holdTime = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //handling player movement on key press
        if (Input.GetKey(left))
        {
            RB.velocity = new Vector2(-playerSpeed, RB.velocity.y);
        }
        if (Input.GetKey(right))
        {
            RB.velocity = new Vector2(playerSpeed, RB.velocity.y);
        }
        if (!Input.GetKey(right) && !Input.GetKey(left))
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
        }

        if (Input.GetKeyDown(action))
        {
            startTime = Time.time;
            
        }
        if (Input.GetKeyUp(action))
        {
            if (startTime + holdTime <= Time.time)
            {
                //Bark
                Debug.Log("Bark");
            }
            else
            {
                //No Bark
                Debug.Log("No Bark");
                if (!isHoldingDuck && canPickup)
                {
                    heldDuck = grabbableDuck;
                    heldDuck.GetComponent<DuckBehaviour>().isHeld = true;
                    isHoldingDuck = true;
                }
                else if (isHoldingDuck)
                {
                    isHoldingDuck = false;
                    heldDuck.GetComponent<DuckBehaviour>().isHeld = false;
                    //heldDuck = null;
                }
            }
        }

        if (isHoldingDuck)
        {
            heldDuck.transform.position = holdPoint.position;
        }

        //ignore collision with other player
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(8, 11);
        Physics2D.IgnoreLayerCollision(9, 11);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canPickup = true;
        grabbableDuck = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPickup = false;
        grabbableDuck = null;
    }
}
