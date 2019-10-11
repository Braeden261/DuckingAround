using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duck;
    public GameObject superDuck;
    public bool haltDucks;
    public float spawnTime;
    public float spawnDelay;
    public float duckChance;
    private int randNum;
    private Vector3 randPos;
    private float timer;
    public Text timerText;
    //private GameObject[] remainingDucks;

    // Start is called before the first frame update
    void Start()
    {
        timer = 120;
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
            CancelInvoke("SpawnDuck");
    }
    void SpawnDuck()
    {
        randNum = Random.Range(0, 100);
        randPos = new Vector3(Random.Range(-460f, 460f), 560f, 0);
        if (randNum <= duckChance)
            Instantiate(superDuck, randPos, transform.rotation);
        else
            Instantiate(duck, randPos, transform.rotation);

        if (haltDucks)
        {
            CancelInvoke("SpawnDuck");
            //remainingDucks = GameObject.FindGameObjectsWithTag("Duck");
            //for (int i = 0; i < remainingDucks.Length; i++)
            //{
            //    Destroy(remainingDucks[i].gameObject);
            //}
        }
    }
}
