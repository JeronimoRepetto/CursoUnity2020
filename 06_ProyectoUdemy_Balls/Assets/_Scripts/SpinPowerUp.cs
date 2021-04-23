using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPowerUp : MonoBehaviour
{
    private float rotationSpeed = 180f;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
