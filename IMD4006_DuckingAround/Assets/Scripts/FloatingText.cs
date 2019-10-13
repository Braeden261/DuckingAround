using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifeSpan;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
}
