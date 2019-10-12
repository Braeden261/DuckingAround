using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duck;
    public GameObject superDuck;
    public float spawnTime;
    public float spawnDelay;
    public bool haltDucks;
    public int duckChance;
    public Text timerText;

    private int randRoll;
    private Vector3 randPos;
    private float delta;
    private int deltaThreshold;
    private float timer;
    private GameObject[] allDucks;

    // Start is called before the first frame update
    void Start()
    {
        timer = 120;
        deltaThreshold = 50;
        InvokeRepeating("SpawnDuck", spawnTime, spawnDelay);
    }

    void Update()
    {
        if (timer > 0)
        {
            //increment timer
            timer -= Time.deltaTime;
            timerText.text = "Duck Season Ends In: \n" + Mathf.Round(timer);
        }
        else
            haltDucks = true;

        if (haltDucks)
        {
            CancelDucks();
        }
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
            Debug.Log("RandPos: " + randPos + ", DuckPos: " + allDucks[i].gameObject.transform.position + ", Delta: " + delta);
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
                Debug.Log("New Duck Position: " + randPos);
            }
        }

        //roll chance of spawning super duck
        randRoll = Random.Range(0, 100);
        //spawn a duck at random position based on which type of duck has been rolled
        if (randRoll <= duckChance)
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

        ////get all remaining ducks and destory them
        //remainingDucks = GameObject.FindGameObjectsWithTag("Duck");
        //for (int i = 0; i < remainingDucks.Length; i++)
        //{
        //    Destroy(remainingDucks[i].gameObject);
        //}
    }
}
