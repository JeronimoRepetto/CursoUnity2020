using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string SPEED_FORWARE = "Speed_f";
    private const string START_RUN = "StartRun";
    private const string ON_GROUND = "Grounded";
    private Animator _animator;
    public Animator animator { get => _animator; }
    [SerializeField]
    private float gameTime;
    [SerializeField]
    public float speedChange = 1f;
    [SerializeField]
    private float animationSpeedChange = 0.1f;
    private float _allSpeed;
    public float allSpeed { get => _allSpeed; }
    private float animationSpeed;
    private float animationSpeedLimit = 5f;
    private bool nextTime = false;
    private GameObject playerObject;
    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        _animator = playerObject
            .GetComponent<Animator>();
        _allSpeed = animator.GetFloat(SPEED_FORWARE);
        animationSpeed = animator.GetFloat(SPEED_FORWARE);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime >= 20f && !playerObject
            .GetComponent<PlayerController>().GameOver)
        {
            if (animator.GetBool(ON_GROUND))
            {
                NextLevel();
            }
            else
            {
                nextTime = true;
            }
        }
        else if (nextTime) 
        {
            nextTime = false;
            NextLevel();
        }
    }

    void NextLevel() 
    {
        level++;
        Debug.Log("¡¡NIVEL: " + level.ToString() + "!!" );
        GameObject[] allObstacles = GameObject.
            FindGameObjectsWithTag("Obstacle");
        foreach (GameObject gameO in allObstacles) {
            Destroy(gameO.gameObject);
        }
        gameTime = 0f;
        if (!animator.GetBool(START_RUN))
        {
            animator.SetBool(START_RUN, true);
            playerObject.GetComponent<PlayerController>()
                .walkSmoke.Stop();
            playerObject.GetComponent<PlayerController>()
                .runSmoke.Play();
        }
        else
        {
            _allSpeed += speedChange;
            if (animationSpeed < animationSpeedLimit)
            {
                animationSpeed += animationSpeedChange;
                animator.SetFloat(SPEED_FORWARE, animationSpeed);
            }
        }
    }
}
