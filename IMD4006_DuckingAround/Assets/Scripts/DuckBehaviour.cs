using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour
{
    public float fallSpeed;
    public bool isSuper;
    public int lifespan;
    public int value;
    public bool isHeld;
    public int spoilRate;
    public int bestBefore;

    private float floor;
    private int life;


    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        life = lifespan;
        floor = -227f;

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
