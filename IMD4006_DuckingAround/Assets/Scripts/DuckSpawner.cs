using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duck;
    public GameObject superDuck;
    public GameObject timerText;
    public float spawnTime;
    public float spawnDelay;
    public bool haltDucks;
    public float luck;
    public float timeLimit;

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
        deltaThreshold = 50;
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
            haltDucks = true;

        if (haltDucks)
        {
            CancelDucks();
        }

        if (timer > 10)
            superDuckChance = luck + ((1 / Mathf.Pow(timer / timeLimit, 2)) / 10);
    }

    void SpawnDuck()
    {
        //generate random lateral position for the new duck
        randPos = new Vector3(Random.Range(-460, 460), 560, 0);
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
                //reroll a new position while the distance is within the threshold
                while (delta < deltaThreshold)
                {
                    //new position
                    randPos = new Vector3(Random.Range(-460, 460), 560, 0);
                    //new delta to verify against
                    delta = Vector3.Distance(randPos, allDucks[i].gameObject.transform.position);
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
