using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBehaviour : MonoBehaviour
{
    public float lifeSpan;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
}