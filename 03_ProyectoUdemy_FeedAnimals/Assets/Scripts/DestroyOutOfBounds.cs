using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    private float topBound = 30f, bottomBound = -6f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.z < bottomBound)
        {
            Debug.Log("Game Over");
            Destroy(this.gameObject);
            Time.timeScale = 0;
        }
    }
}
