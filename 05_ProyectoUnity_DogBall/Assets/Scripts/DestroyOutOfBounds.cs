using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float maxPositionX = 26f;
    private float maxPositionY = -10f;
    private void Update()
    {
        if (transform.position.x <= -maxPositionX) 
        {
            Destroy(gameObject);
        }


        if (transform.position.x >= maxPositionX)
        {
            Destroy(gameObject);
        }

        if (transform.position.y <= maxPositionY) 
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0;
        }
    }
}
