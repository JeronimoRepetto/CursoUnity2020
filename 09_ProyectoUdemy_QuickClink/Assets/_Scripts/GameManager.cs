using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        loading,
        inGame,
        gameOver,
        pause
    }
    [SerializeField]
    private GameState _gameState;
    public GameState gameState { get => _gameState; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button resetButton;
    private GameObject followMouseObject;
    [SerializeField]
    private List<GameObject> objects;
    [SerializeField, Range(0.1f, 10f)]
    private float spawnSeconds = 1f;
    private float xPosition = 4f;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.inGame;
        resetButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        UpdateScore(0);
        StartCoroutine(spawnObjects());
        followMouseObject = GameObject.Find("FollowMouse");
    }

    // Update is called once per frame
    void Update()
    {
        followMouseObject.transform.position = MousePosition();
    }

    /// <summary>
    /// Encuentra la posicion actual del mouse en la pantalla
    /// </summary>
    /// <returns>
    /// Devuelve un Vector3 con las coordinadas del Mouse en cada frame.
    /// Posicion Z la settea para que se coloque delante de los objetos
    /// </returns>
    private Vector3 MousePosition() 
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -5f;
        return mousePosition;
    }

    /// <summary>
    /// Genera un Vector3 de forma aleatoria
    /// </summary>
    /// <returns>Devuevlve una posicion aleatoria</returns>
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xPosition, xPosition), -5f);
    }

    /// <summary>
    /// Instancia un objeto aleatorio
    /// </summary>
    /// <returns>Devuelve la instancia de un objeto</returns>
    IEnumerator spawnObjects()
    {
        while (_gameState.Equals(GameState.inGame))
        {
            int randomNum = Random.Range(0, objects.Count);
            yield return new WaitForSeconds(spawnSeconds);
            Instantiate(objects[randomNum], RandomPosition(), objects[randomNum].transform.rotation);
        }

    }

    /// <summary>
    /// Actualiza la puntación 
    /// </summary>
    /// <param name="point">Número de punto a añadir al score</param>
    public void UpdateScore(int point) 
    {
        score += point;
        scoreText.text = "Score:\n" + score;
    }

    /// <summary>
    /// Finalizacion del juego
    /// </summary>
    public void ChangeGameOver( ParticleSystem lastExplode = null, Vector3 lastPosition = new Vector3()) 
    {
        if (lastExplode != null)
        {
            Instantiate(lastExplode, lastPosition, lastExplode.transform.rotation);
            StartCoroutine(LastSeconds(lastExplode.main.duration));
        }
        _gameState = GameState.gameOver;
        scoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        if (lastExplode == null)
        {
            StartCoroutine(LastSeconds(0));
        }
    }

    /// <summary>
    /// Resetea el juego
    /// </summary>
    public void RestartGame() 
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
        Time.timeScale = 1;
    }

    /// <summary>
    /// Espera el tiempo que dura la animacion x 2 para pausar el juego
    /// </summary>
    /// <param name="explosionTime">Tiempo de duracion de explosion</param>
    /// <returns>Pausa el tiempo</returns>
    IEnumerator LastSeconds(float explosionTime) 
    {
        yield return new WaitForSeconds(explosionTime * 2);
        Time.timeScale = 0;
    }
}
