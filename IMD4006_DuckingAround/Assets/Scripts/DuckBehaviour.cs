using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour
{
    public Sprite[] sprites;
    public float fallSpeed;
    public bool isSuper;
    public int lifespan;
    public int value;
    public bool isHeld;
    public int spoilRate;
    public int bestBefore;

    private float floor;
    private int life;
    private SpriteRenderer SR;


    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        life = lifespan;
        floor = -260f;

        SR = GetComponent<SpriteRenderer>();

        //method of reducing a duck's value over time until it despawns
        InvokeRepeating("Spoil", 1, spoilRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > floor && !isHeld)
        {
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
        }

        SetSprite();
    }

    void SetSprite()
    {
        if (transform.position.y > floor && !isHeld)
        {
            SR.sprite = sprites[0];
        }
        else if (transform.position.y <= floor)
        {
            transform.position = new Vector3(transform.position.x, floor, transform.position.z);

            switch (life)
            {
                case 1:
                    SR.sprite = sprites[5];
                    break;
                case 2:
                    SR.sprite = sprites[4];
                    break;
                case 3:
                    SR.sprite = sprites[3];
                    break;
                case 4:
                    SR.sprite = sprites[2];
                    break;
                default:
                    SR.sprite = sprites[1];
                    break;
            }
        }
    }

    void Spoil()
    {
        if (transform.position.y <= floor && !isHeld && life > 0)
        {
            life--;
            if (!isSuper && life < bestBefore)
            {
                value--;
            }
        }
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
