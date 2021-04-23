using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    [SerializeField, Range(0, 4)]
    private float minSpawnTime = 0.5f;
    [SerializeField, Range(0, 10)]
    private float maxSpawnTime = 3f;
    private float time;
    private float randomTime = 2f;
    private float minYPosition = 5f;
    private float maxYPosition = 14f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= randomTime)
        {
            time = 0;
            randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            SpawnObjects();
        }
    }

    void SpawnObjects() 
    {
        Vector3 spawnLocation = new Vector3(transform.position.x,
            Random.Range(minYPosition, maxYPosition), 
            transform.position.z);

        int index = Random.Range(0, objectPrefabs.Length);
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
