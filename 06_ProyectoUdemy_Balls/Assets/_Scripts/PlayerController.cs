using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private float force;
    private float verticalInput;
    private Rigidbody _rb;
    private GameObject focalPoint;
    private bool havePowerUp = false;
    private float powerUpForce = 40f;
    [SerializeField]
    private float powerUpTime = 10f;
    [SerializeField]
    private GameObject[] powerIndicator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        _rb.AddForce(focalPoint.transform.forward * force * verticalInput,ForceMode.Force);
        foreach (GameObject indicator in powerIndicator) 
        {
            indicator.transform.position = transform.position;
        }

        if (transform.position.y <= -10) 
        {
            Debug.Log("GAME OVER");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "PowerUp":
                havePowerUp = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerUpCountdown());
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && havePowerUp) 
        {
            Rigidbody enemyRb =  collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - this.transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);               
        }
    }

    //CORRUTINA
    IEnumerator PowerUpCountdown()
    {
        foreach (GameObject indicator in powerIndicator) 
        {
            indicator.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(powerUpTime / powerIndicator.Length);
            indicator.gameObject.SetActive(false);
        }
        havePowerUp = false;
    }
}
