using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float minXPosition = -20f;
    [SerializeField]
    private float maxXPosition = 8f;
    private float count;
    private float waitTime = 4.0f;
    public GameObject[] balls;

    private void Update()
    {
        count += Time.deltaTime;

        if (count >= waitTime)
        {
            count = 0;
            waitTime = Random.Range(1, 5);
            SpawnBall();
        }
    }
    // Update is called once per frame
    void SpawnBall()
    {
            Instantiate(balls[Random.Range(0, balls.Length)],
                new Vector3(Random.Range(minXPosition, maxXPosition),transform.position.y,transform.position.z),
                Quaternion.identity);
    }
}
