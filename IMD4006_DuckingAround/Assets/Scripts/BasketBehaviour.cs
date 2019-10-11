using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketBehaviour : MonoBehaviour
{
    private int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: \n" + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<DuckBehaviour>().isHeld)
        {
            score += collision.gameObject.GetComponent<DuckBehaviour>().value;
            Destroy(collision.gameObject);
        }
    }
}
