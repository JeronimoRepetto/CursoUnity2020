using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField, Range(10f, 100f)]
    private float maxInpulseForce = 16f;
    [SerializeField, Range(10f, 100f)]
    private float minImpulseForce = 5f;
    [SerializeField, Range(0f, 20f)]
    private float torqueForce = 15f;
    private Rigidbody objectRb;
    private GameManager gameManager;
    [SerializeField]
    private ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objectRb = GetComponent<Rigidbody>();
        objectRb.AddForce(randomInpulseForce(), ForceMode.Impulse);
        objectRb.AddTorque(randomTorqueForce(), ForceMode.Impulse);
    }

    /// <summary>
    /// Genera una fuerza de impulso aleatoriamente en el eje Y
    /// </summary>
    /// <returns>Devuelve un Vector3 aleatoriamente</returns>
    private Vector3 randomInpulseForce()
    {
        return Vector3.up * Random.Range(minImpulseForce, maxInpulseForce);
    }

    /// <summary>
    /// Genera una fuerza de torque aleatoriamente
    /// </summary>
    /// <returns>Devuelve un Vector3 aletaroiamente</returns>
    private Vector3 randomTorqueForce()
    {
        return new Vector3(Random.Range(-torqueForce, torqueForce),
            Random.Range(-torqueForce, torqueForce),
            Random.Range(-torqueForce, torqueForce)
            );
    }

    private void Update()
    {
        if (this.transform.position.y <= -30f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (gameManager.gameState.Equals(GameManager.GameState.inGame))
            switch (this.gameObject.tag)
            {
                case "Food":
                    Destroy(this.gameObject);
                    gameManager.UpdateScore(1);
                    break;
                case "Bomb":
                    Destroy(this.gameObject);
                    gameManager.ChangeGameOver(explosionParticle, transform.position);
                    break;
                case "PowerUp":
                    break;
                case "RandomPowerUp":
                    break;
            }
        InstatiateExplosion();
    }

    /// <summary>
    /// Instancia la animacion de las particulas explotando
    /// </summary>
    void InstatiateExplosion()
    {
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    }
}
