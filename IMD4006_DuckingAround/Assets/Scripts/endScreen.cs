using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScreen : MonoBehaviour
{

    public GameObject bgImage;
    public Sprite P1Win;
    public Sprite P2Win;
    public Sprite Tie;

    public AudioSource EndSound;
    //
    public AudioClip WinSound;
    //
    public AudioClip TieSound;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("p1Score") > PlayerPrefs.GetInt("p2Score"))
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = P1Win;
            EndSound.GetComponent<AudioSource>().clip = WinSound;
        }
        else if (PlayerPrefs.GetInt("p1Score") < PlayerPrefs.GetInt("p2Score"))
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = P2Win;
            EndSound.GetComponent<AudioSource>().clip = WinSound;

        }
        else
        {
            bgImage.GetComponent<SpriteRenderer>().sprite = Tie;
            EndSound.GetComponent<AudioSource>().clip = TieSound;

        }
        EndSound.Play();
    }

    // Update is called once per frame
    void Update()
    {



    }
}
