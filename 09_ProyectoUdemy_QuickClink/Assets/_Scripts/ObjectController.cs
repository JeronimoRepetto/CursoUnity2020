using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField, Range(10f, 100f)]
    private float maxInpulseForce = 16f;
    [SerializeField, Range(10f, 100f)]
    private float minImpulseForce = 5f;
    [SerializeField, Range(10f, 100f)]
    private float maxInpulseForceX = 5f;
    [SerializeField, Range(10f, 100f)]
    private float minImpulseForceX = 0f;
    [SerializeField, Range(0f, 20f)]
    private float torqueForce = 5f;
    private Rigidbody objectRb;
    private GameManager gameManager;
    [SerializeField]
    private ParticleSystem explosionParticle;
    private int point = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objectRb = GetComponent<Rigidbody>();
        objectRb.AddForce(randomInpulseForceY(), ForceMode.Impulse);
        objectRb.AddForce(randomInpulseForceX(), ForceMode.Impulse);
        objectRb.AddTorque(randomTorqueForce(), ForceMode.Impulse);
        SetValPoint();
    }

    private void Update()
    {
        if (this.transform.position.y <= -30f)
        {
            Destroy(this.gameObject);
            LoseLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            Destroy(this.gameObject);
            LoseLife();
        }
    }

    private void OnMouseOver()
    {
        if (gameManager.gameState.Equals(GameManager.GameState.inGame))
            switch (this.gameObject.tag)
            {
                case "Food":
                    Destroy(this.gameObject);
                    gameManager.UpdateScore(point);
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
    /// Settea los puntos que da la comida segun la dificultad
    /// </summary>
    void SetValPoint() 
    {
        switch (gameManager.difficulty)
        {
            case GameManager.GameMode.easy:
                point = 5;
                break;
            case GameManager.GameMode.normal:
                point = 2;
                break;
            case GameManager.GameMode.hard:
                point = 2;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Llama al metodo del GameManager para quitar una vida segun la dificultad
    /// </summary>
    void LoseLife() 
    {
        if (gameManager.gameState.Equals(GameManager.GameState.inGame) 
            && this.gameObject.CompareTag("Food"))
        {
            if (gameManager.difficulty.Equals(GameManager.GameMode.easy)
                || gameManager.difficulty.Equals(GameManager.GameMode.normal))
            {
                gameManager.LoseLife();
            }
            else 
            {
                gameManager.ChangeGameOver();
            }
        }
    }

    /// <summary>
    /// Genera una fuerza de impulso aleatoriamente en el eje Y
    /// </summary>
    /// <returns>Devuelve un Vector3 aleatoriamente</returns>
    private Vector3 randomInpulseForceY()
    {
        return Vector3.up * Random.Range(minImpulseForce, maxInpulseForce);
    }

    /// <summary>
    /// Genera una fuerza de impulso aleatoriamente en el eje Y
    /// </summary>
    /// <returns>Devuelve un Vector3 aleatoriamente</returns>
    private Vector3 randomInpulseForceX()
    {
        Vector3[] arrayVector3 = { Vector3.left, Vector3.right};
        int position = Random.Range(0, arrayVector3.Length);
        return arrayVector3[position] * Random.Range(minImpulseForceX, maxInpulseForceX);
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

    /// <summary>
    /// Instancia la animacion de las particulas explotando
    /// </summary>
    void InstatiateExplosion()
    {
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    }
}
