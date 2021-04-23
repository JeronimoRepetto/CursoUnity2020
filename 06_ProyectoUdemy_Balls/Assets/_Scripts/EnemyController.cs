using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Range(0,50)]
    private float forceForward;
    private GameObject player;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - this.transform.position)
            .normalized;
        _rb.AddForce(lookDirection * forceForward, ForceMode.Force);
        checkBounds();
    }

    void checkBounds() 
    {
        if (transform.position.y <= -10f) 
        {
            Destroy(this.gameObject);
        }
    }
}
