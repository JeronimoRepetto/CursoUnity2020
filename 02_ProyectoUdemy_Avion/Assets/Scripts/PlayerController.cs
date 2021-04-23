using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Propiedades - siempre van con corchetes. SerializeField = se muestra variable en editor aunque esta sea privada
    [SerializeField, Range(0, 60)]
    private float speed = 0f, turnSpeed = 90f, engineSpeed = 0f;
    [SerializeField]
    private float horizontalInput, frontInput, verticalInput, rotationInput;
    public Rigidbody rb;
    public GameObject child;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        frontInput = Input.GetAxis("Vertical");
        verticalInput = Input.GetAxis("Mouse Y");
        rotationInput = Input.GetAxis("Mouse X");
        horizontalInput = Input.GetAxis("Horizontal");
        //Time.deltaTime = Tiempo de ejecucion no FPS
        if (frontInput > 0)
        {
            if (index == 0) 
            {
                index++;
                engineSpeed = 100f;
            }
            if (speed != 60) 
            {
                speed = speed + 0.5f;
                engineSpeed += 10;
            }
            rb.useGravity = false;
        }
        else {
            speed = 0f;
            if (engineSpeed != 0) engineSpeed -= 5;
            index = 0;
            rb.useGravity = true;
        }
        transform.Translate(speed * Time.deltaTime * Vector3.forward * frontInput);
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up * horizontalInput);
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.right * verticalInput * 10);
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.back * rotationInput * 10);
        child.transform.Rotate(engineSpeed * Time.deltaTime * Vector3.back);

    }
}
