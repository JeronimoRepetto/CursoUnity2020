using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectile")) 
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
