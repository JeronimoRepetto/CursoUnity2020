using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;
    public ParticleSystem explosionParticle;
    public ParticleSystem coinParticle;
    private Rigidbody playerRb;
    private float gravityModifier = 1.5f;
    private bool _gameOver = false;
    public bool gameOver { get => _gameOver; }
    [SerializeField]
    private float force;
    [SerializeField]
    private float constanteForce;
    private float littleFlorce;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        force = 150f;
        constanteForce = 2f;
        littleFlorce = 100f;
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent < AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) && !_gameOver)
       {
           playerRb.AddForce(Vector3.up * force);
       }
        if (Input.GetKey(KeyCode.Space) && !_gameOver)
        {
            playerRb.AddForce(Vector3.up * constanteForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Money":
                CoinEfect(collision);
                break;
            case "Bomb":
                GameOver(collision);
                break;
            case "EndBackground":
                GameOver(collision);
                break;
            case "Ground":
                BoingEfect();
                break;
            default:
                break;
        }
    }

    void GameOver(Collision collision) 
    {
        explosionParticle.Play();
        playerRb.useGravity = false;
        playerAudio.PlayOneShot(explodeSound, 1.0f);
        _gameOver = true;
        Debug.Log("Game Over!");
        Destroy(collision.gameObject);
    }

    void BoingEfect() 
    {
        playerAudio.PlayOneShot(boingSound, 1.0f);
        playerRb.AddForce(Vector3.up * littleFlorce);
    }

    void CoinEfect(Collision collision) 
    {
        coinParticle.Play();
        playerAudio.PlayOneShot(moneySound, 1.0f);
        Destroy(collision.gameObject);
    }
}
