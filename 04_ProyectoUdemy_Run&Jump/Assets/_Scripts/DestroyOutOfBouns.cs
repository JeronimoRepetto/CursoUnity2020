using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBouns : MonoBehaviour
{
    private float endPositionX = -2;
    void Update()
    {
        if(transform.position.x <= endPositionX) Destroy(gameObject);
    }
}
