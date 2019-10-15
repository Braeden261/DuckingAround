using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketBehaviour : MonoBehaviour
{

    public AudioSource pointFX;
    public int score;
    public GameObject scoreText;
    public GameObject floatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 1000)
            scoreText.GetComponent<TextMesh>().text = "" + score;
        else if (score >= 100)
            scoreText.GetComponent<TextMesh>().text = "0" + score;
        else if (score >= 10)
            scoreText.GetComponent<TextMesh>().text = "00" + score;
        else if(score >= 0)
            scoreText.GetComponent<TextMesh>().text = "000" + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<DuckBehaviour>().isHeld)
        {
            pointFX.Play();
            score += collision.gameObject.GetComponent<DuckBehaviour>().value;
            floatingTextPrefab.GetComponent<TextMesh>().text = "+" + collision.gameObject.GetComponent<DuckBehaviour>().value;
            Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            Destroy(collision.gameObject);
        }
    }
}
