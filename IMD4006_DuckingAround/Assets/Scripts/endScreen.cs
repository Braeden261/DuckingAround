using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScreen : MonoBehaviour
{

    public GameObject bgImage;
    public Sprite P1Win;
    public Sprite P2Win;
    public Sprite Tie;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("p1Score") > PlayerPrefs.GetInt("p2Score"))
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = P1Win;
        }
        else if (PlayerPrefs.GetInt("p1Score") < PlayerPrefs.GetInt("p2Score"))
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = P2Win;

        }
        else
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = Tie;

        }

    }
}
