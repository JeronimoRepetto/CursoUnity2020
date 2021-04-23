using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForware : MonoBehaviour
{
    [SerializeField]
    private float speed = 40;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
