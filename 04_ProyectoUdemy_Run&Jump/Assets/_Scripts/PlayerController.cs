using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //PARAMETRO DE ANIMACIONES
    private const string SPEED_FORWARE = "Speed_f";
    private const string START_RUN = "StartRun";
    private const string DEATH_TYPE = "DeathType_int";
    private const string IS_DEATH = "Death_b";
    private const string JUMP = "Jump_trig";
    private const string ON_GROUND = "Grounded";
    //
    private Rigidbody rbPlayer;
    [SerializeField]
    private float jumpForce = 6f;
    public float gravityMultiplier;
    private bool onGround;
    private Animator animator;
    public ParticleSystem explosion;
    public ParticleSystem walkSmoke;
    public ParticleSystem runSmoke;

    private bool _gameOver = false;
    public bool GameOver { get => _gameOver;}

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
        animator = GetComponent<Animator>();
        walkSmoke.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !_gameOver)
        {
            onGround = false;
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger(JUMP);
            animator.SetBool(ON_GROUND, onGround);
            if (animator.GetBool(START_RUN))
            {
                runSmoke.Stop();
            }
            else 
            {
                walkSmoke.Stop();
            }            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !GameOver) 
        {
            onGround = true;
            animator.SetBool(ON_GROUND, onGround);
            if (animator.GetBool(START_RUN))
            {
                runSmoke.Play();
            }
            else
            {
                walkSmoke.Play();
            }            
        }
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            if (animator.GetBool(START_RUN))
            {
                runSmoke.Stop();
            }
            else
            {
                walkSmoke.Stop();
            }
            explosion.Play();
            _gameOver = true;
            animator.SetInteger(DEATH_TYPE, Random.Range(1, 3));
            animator.SetBool(IS_DEATH, true);
            Debug.Log("¡GAME OVER!");
        }
    }
}
