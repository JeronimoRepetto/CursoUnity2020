using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    private int enemyCount;
    private int powerUpCount;
    private int waveCount = 1;
    private bool spawnPowerUp = true;
    private float powerDurations = 7f;
    private bool startCoroutine = true;


    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        powerUpCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if (enemyCount == 0) SpawnEnemyWave(waveCount);
        if (powerUpCount == 0 && spawnPowerUp) powerUpSpawn();
        if (powerUpCount == 0 && !spawnPowerUp && startCoroutine)
            StartCoroutine(TimeToSpawn());
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void powerUpSpawn()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        spawnPowerUp = false;
        startCoroutine = true;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        waveCount++;
    }

   // Move player back to position in front of own goal
   // void ResetPlayerPosition()
   // {
   //     player.transform.position = new Vector3(0, 1, -7);
   //     player.GetComponent<Rigidbody>().velocity = Vector3.zero;
   //     player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
   // }
    IEnumerator TimeToSpawn() 
    {
        startCoroutine = false;
        yield return new WaitForSeconds(powerDurations);
        spawnPowerUp = true;
    }

}
