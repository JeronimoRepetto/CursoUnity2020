using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float xPosition = 15f;
    public GameObject proyectilePrefab;
    [SerializeField]
    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(speed * Time.deltaTime * horizontalInput * Vector3.right);

        if (transform.position.x < -xPosition)
        {
            transform.position = new Vector3(-xPosition, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xPosition)
        {
            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Instantiate(proyectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        }
    }
}
