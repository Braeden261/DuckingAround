using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //adding custom controls for each player
    public KeyCode left;
    public KeyCode right;
    public KeyCode action;

    public AudioSource barkFX;

    public float playerSpeed;
    public Transform holdPoint;
    public float cooldownTime = 5.0f;
    public PlayerController enemyPlayer = null;
    public int numBarks = 3;
    public GameObject[] barkIcons;
    public GameObject barkPopup;

    private bool isHoldingDuck;
    private bool canPickup;
    private GameObject grabbableDuck = null;
    private GameObject heldDuck = null;

    private float startTime = 0.0f;
    private float holdTime = 1.0f;
    private float stunStartTime = 0.0f;
    private float stunnedTime = 2.0f;
    private float cooldownStartTime = 0.0f;
    private bool stunned = false;

    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private Animator AN;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        AN = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            if (stunStartTime + stunnedTime <= Time.time)
            {
                stunned = false;
                playerSpeed = 750;
            }
        }

        if (!stunned)
        {
            //handling player movement on key press
            if (Input.GetKey(left))
            {
                RB.velocity = new Vector2(-playerSpeed, RB.velocity.y);
                AN.SetFloat("Speed", playerSpeed);
                SR.flipX = true;
            }
            if (Input.GetKey(right))
            {
                RB.velocity = new Vector2(playerSpeed, RB.velocity.y);
                AN.SetFloat("Speed", playerSpeed);
                SR.flipX = false;
            }
            if (!Input.GetKey(right) && !Input.GetKey(left))
            {
                RB.velocity = new Vector2(0, RB.velocity.y);
                AN.SetFloat("Speed", 0);
            }
            if (Input.GetKeyDown(left) && Input.GetKeyDown(right))
            {
                startTime = Time.time;
            }

            if ((Input.GetKey(left)) && (Input.GetKey(right)) && (cooldownStartTime + cooldownTime <= Time.time))
            {
                RB.velocity = new Vector2(0, RB.velocity.y);
                AN.SetFloat("Speed", 0);
                if ((startTime + holdTime <= Time.time) && (numBarks > 0))
                {
                    Instantiate(barkPopup, holdPoint.transform.position, Quaternion.identity);
                    Destroy(barkIcons[numBarks - 1].gameObject);
                    barkFX.Play();
                    //Debug.Log("Bark");
                    enemyPlayer.Stun();
                    numBarks--;
                    cooldownStartTime = Time.time;
                }
            }

            if (Input.GetKeyDown(action))
            {
                if (!isHoldingDuck)
                {
                    AN.SetBool("Pickup", true);
                    if (canPickup && grabbableDuck)
                    {
                        heldDuck = grabbableDuck;
                        heldDuck.GetComponent<DuckBehaviour>().isHeld = true;
                        isHoldingDuck = true;
                    }
                }
                else if (isHoldingDuck)
                {
                    isHoldingDuck = false;
                    heldDuck.GetComponent<DuckBehaviour>().isHeld = false;
                    //heldDuck = null;
                }
            }

            if (Input.GetKeyUp(action))
                AN.SetBool("Pickup", false);

            if (isHoldingDuck)
                heldDuck.transform.position = holdPoint.position;

            barkPopup.transform.position = holdPoint.transform.position;

            //ignore collision with other player
            Physics2D.IgnoreLayerCollision(8, 9);
            Physics2D.IgnoreLayerCollision(8, 11);
            Physics2D.IgnoreLayerCollision(9, 11);
        }
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

    public void Stun()
    {
        if (isHoldingDuck)
        {
            playerSpeed = 0;
            isHoldingDuck = false;
            heldDuck.GetComponent<DuckBehaviour>().isHeld = false;
            //heldDuck = null;
        }
        stunned = true;
        stunStartTime = Time.time;
    }
}