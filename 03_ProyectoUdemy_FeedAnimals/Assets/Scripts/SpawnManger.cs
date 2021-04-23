using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] proyectiles;

    public int enemyIndex, protectiles;

    [SerializeField]
    private float spawnRangeX = 1f;
    private float spawnPositionZ;
    [SerializeField, Range(1,5)]
    private float startDelay = 2f;
    [SerializeField, Range(0.25f,60f)]
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        enemyIndex = enemies.Length;
        spawnPositionZ = transform.position.z;
        InvokeRepeating("Spawn", startDelay, spawnInterval);
    }


    void Spawn() 
    {
        enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex],
            new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPositionZ),
            enemies[enemyIndex].transform.rotation);
    }
}
