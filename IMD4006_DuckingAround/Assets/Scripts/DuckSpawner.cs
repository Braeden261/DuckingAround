using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DuckSpawner : MonoBehaviour
{
    public AudioSource pointFX;
    public GameObject duck;
    public GameObject superDuck;
    public GameObject timerText;
    public float spawnTime;
    public float spawnDelay;
    public bool haltDucks;
    public float luck;
    public float timeLimit;

    public BasketBehaviour p1Basket;
    public BasketBehaviour p2Basket;

    private float timer;
    private float superDuckChance;
    private float randRoll;
    private Vector3 randPos;
    private float delta;
    private int deltaThreshold;
    private GameObject[] allDucks;

    void Start()
    {
        timer = timeLimit;
        superDuckChance = luck;
        deltaThreshold = 55;
        InvokeRepeating("SpawnDuck", spawnTime, spawnDelay);
    }

    void Update()
    {
        if (timer > 0)
        {
            //decrement timer
            timer -= Time.deltaTime;
            timerText.GetComponent<TextMesh>().text = "" + Mathf.Round(timer);
        }
        else
        {
            haltDucks = true;
        }

        if (haltDucks)
        {
            CancelDucks();
        }

        //increase the chance of a super duck falling over time
        if (timer > 9)
            superDuckChance = luck + ((1 / Mathf.Pow(timer / timeLimit, 2)) / 10);

        if (timer < 0) 
        {
            if (GameObject.FindGameObjectsWithTag("Duck").Length == 0)
            {
                PlayerPrefs.SetInt("p1Score", p1Basket.score);
                PlayerPrefs.SetInt("p2Score", p2Basket.score);

                SceneManager.LoadScene(sceneName: "EndScreen");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("space") || Input.GetKeyDown("r"))
        {

            SceneManager.LoadScene(sceneName: "DuckingAroundScene");
        }
    }

    void SpawnDuck()
    {
        //generate random lateral position for the new duck
        randPos = new Vector3(Random.Range(-500, 500), 560, 0);
        //get all the currently spawned ducks
        allDucks = GameObject.FindGameObjectsWithTag("Duck");

        //verify new position against all other ducks
        for (int i = 0; i < allDucks.Length; i++)
        {
            //get lateral distance between new random position and duck[i] in array of all ducks
            delta = Mathf.Abs(randPos.x - allDucks[i].gameObject.transform.position.x);
            //check that the distance is outside of a threshold
            if (delta < deltaThreshold)
            {
                //reroll a new position if the distance is within the threshold -- limit loops to the number of ducks there are
                foreach (GameObject duck in allDucks)
                {
                    if (delta < deltaThreshold)
                    {
                        //new position
                        randPos = new Vector3(Random.Range(-460, 460), 560, 0);
                        //new delta to verify against
                        delta = Vector3.Distance(randPos, allDucks[i].gameObject.transform.position);
                    }
                }
                i = -1;
            }
        }

        //roll chance of spawning super duck
        randRoll = Random.Range(0, 100);

        //spawn a duck at random position based on which type of duck has been rolled
        if (randRoll <= superDuckChance)
        {
            Instantiate(superDuck, randPos, transform.rotation);
        }
        else
        {
            Instantiate(duck, randPos, transform.rotation);
        }
    }

    void CancelDucks()
    {
        //cancel the duck spawner
        CancelInvoke("SpawnDuck");
    }
}
