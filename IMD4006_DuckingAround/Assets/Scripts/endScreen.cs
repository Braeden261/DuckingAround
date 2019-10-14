using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("space") || Input.GetKeyDown("r"))
        {

            SceneManager.LoadScene(sceneName: "DuckingAroundScene");
        }


    }
}
