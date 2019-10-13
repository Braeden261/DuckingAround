﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScreen : MonoBehaviour
{

    public GameObject bgImage;
    public Sprite P1Win;
    public Sprite P2Win;
    public Sprite Tie;
    private BasketBehaviour[] baskets;

    // Start is called before the first frame update
    void Start()
    {
        baskets = endScreen.FindObjectsOfType<BasketBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

        if (baskets[0].score > baskets[1].score)
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = P1Win;
        }
        else if (baskets[0].score < baskets[1].score)
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = P2Win;

        }
        else
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = Tie;

        }

    }
}
