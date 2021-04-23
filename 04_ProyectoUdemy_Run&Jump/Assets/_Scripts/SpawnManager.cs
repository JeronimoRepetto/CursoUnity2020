using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePf;
    private float count;
    private float randomTime = 1;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
    }
    void Update()
    {
        count += Time.deltaTime;
        if (count >= randomTime && !_playerController.GameOver) 
        {
            count = 0;
            randomTime = Random.Range(1.5f, 3);
            SpawnObstacle();
        }
        
    }

    void SpawnObstacle() 
    {
        GameObject obstacle = obstaclePf[Random.Range(0, obstaclePf.Length)];
        Instantiate(obstacle, 
            new Vector3(transform.position.x, obstacle.transform.position.y, obstacle.transform.position.z),
            obstacle.transform.rotation);
    }
}
