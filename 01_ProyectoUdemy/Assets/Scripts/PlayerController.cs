using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //Propiedades - siempre van con corchetes. SerializeField = se muestra variable en editor aunque esta sea privada
    [Range(0,20), SerializeField, Tooltip("Velocidad actual del auto")]
    private float speed = 10f;
    [Range(0, 25),SerializeField, Tooltip("Velocidad de giro del auto")]
    private float turnSpeed = 5;
    private float horizontalInput, verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        //Time.deltaTime = Tiempo de ejecucion no FPS
        transform.Translate(speed * Time.deltaTime * Vector3.forward * verticalInput);
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up * horizontalInput);

    }

}
