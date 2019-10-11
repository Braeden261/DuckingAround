using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour
{
    //number of points a player will get
    public int value;
    //how fast the object falls
    public float fallSpeed;
    //if the duck is beign held by a player
    public bool isHeld;
    //if the duck is super or not
    public bool isSuper;
    //threshold to fall to
    private float floor;
    public float lifespan;
    private float life;


    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        life = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSuper)
            floor = -263f;
        else
            floor = -283f;

        if (transform.position.y > floor && !isHeld)
        {
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
        }
        
        if (transform.position.y <= floor && !isHeld && life > 0)
        {
            life -= Time.deltaTime;
        }
        if  (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
