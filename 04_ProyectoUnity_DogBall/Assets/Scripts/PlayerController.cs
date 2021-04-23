using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public GameObject dog;
    private float spawnDog = 25f;
    private bool directionSpawn = false;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            directionSpawn = !directionSpawn;
            Vector3 dogPos = new Vector3( 
                                         directionSpawn 
                                         ? spawnDog 
                                         : -spawnDog,
                                         transform.position.y, 
                                         transform.position.z
                                        );

            Quaternion dogRot = directionSpawn 
                                                ? dog.transform.rotation 
                                                : new Quaternion( - dog.transform.rotation.x,
                                                                 90f,
                                                                 - dog.transform.rotation.z,
                                                                 90f
                                                                );
           
            Instantiate(dog, dogPos, dogRot);
               
        }

    }
}
