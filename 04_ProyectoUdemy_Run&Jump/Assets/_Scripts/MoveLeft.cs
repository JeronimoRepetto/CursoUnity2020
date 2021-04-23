using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private const string START_RUN = "StartRun";
    private PlayerController _playerController;
    public float speed;
    private GameManager gameManager;
    private bool isRun = false;
    private float originalSpeed;
    private float limitSpeed = 100f;

    private void Start()
    {
        originalSpeed = 4f;
        _playerController = GameObject.FindWithTag("Player")
            .GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameManager>();
        
        
    }
    void Update()
    {
        if (gameManager.animator.GetBool(START_RUN) && !isRun) 
        {
            isRun = true;
            originalSpeed = 10f;
        }
        if(speed < limitSpeed)
            speed = originalSpeed + (gameManager.allSpeed * 2);

        if (!_playerController.GameOver)
            transform.Translate(speed * Vector3.left * Time.deltaTime);       
    }
}
