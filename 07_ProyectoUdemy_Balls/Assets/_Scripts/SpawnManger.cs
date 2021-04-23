using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject powerUpPrefab;
    private float spawnRange = 9;
    [SerializeField, Range(0, 10)]
    private float spawnTime;
    private int cantEnemies = 3;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) 
        {
            cantEnemies++;
            SpawnEnemyWave();
            SpawnPowerUp();
        }     
    }

    /// <summary>
    /// Spawnea una cantidad X de enemigos
    /// </summary>
    private void SpawnEnemyWave()
    {
        for (int i = 0; i < cantEnemies; i++) 
        {
            Instantiate(enemyPrefab,GenerateSpawnPosition(),
                enemyPrefab.transform.rotation);
        }
    }
    /// <summary>
    /// Spawnea una unidad de PowerUp
    /// </summary>
    private void SpawnPowerUp()
    {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(),
                powerUpPrefab.transform.rotation);
    }

    /// <summary>
    /// Genera una posicion aleatoria dentro de la zona de juego
    /// </summary>
    /// <returns>Devuelve una posicion aleatoria dentro de la zona de juego (Vector3)</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosx, 0, spawnPosZ);
    }

    //IEnumerator SpawnEnemies() 
    //{
    //        cantEnemies++;
    //        yield return new WaitForSecondsRealtime(1);
    //        SpawnEnemyWave();        
    //}
    //
    //IEnumerator SpawnPowerUps() 
    //{
    //    yield return new WaitForSecondsRealtime(newPoweUpTime);
    //    SpawnPowerUp();
    //    waitPowerUp = true;
    //}
}
