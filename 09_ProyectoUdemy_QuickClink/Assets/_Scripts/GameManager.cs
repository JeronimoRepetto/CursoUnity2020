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

    public enum GameMode
    {
        easy,
        normal,
        hard
    }

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//
    //PARAMETROS//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//
    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//
    //STATUS
    [SerializeField]
    private GameState _gameState;
    public GameState gameState { get => _gameState; }
    private GameMode _difficulty;
    public GameMode difficulty { get => _difficulty; }

    //UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button resetButton;
    public GameObject titleScreen;
    //UI - SCORE
    public TextMeshProUGUI eMaxScoreText;
    public TextMeshProUGUI eMaxScoreNum;
    public TextMeshProUGUI nMaxScoreText;
    public TextMeshProUGUI nMaxScoreNum;
    public TextMeshProUGUI hMaxScoreText;
    public TextMeshProUGUI hMaxScoreNum;
    //---//


    //SCORE
    const string eMaxScore = "EASY_MAX_SCORE";
    const string nMaxScore = "NORMAL_MAX_SCORE";
    const string hMaxScore = "HARD_MAX_SCORE";
    private int score;

    //LIFE
    public List<GameObject> lives;
    public GameObject lifePanel;
    private int livesCount;

    //FOLLOW MOUSE
    private GameObject followMouseObject;

    //SPAWN
    public GameObject bomb;
    [SerializeField, Range(1, 20)]
    private int cantMoreBombs = 5;
    private float xPosition = 4f;
    [SerializeField]
    private List<GameObject> objects;
    [SerializeField, Range(0.1f, 10f)]
    private float spawnSeconds = 1f;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//
    //PARAMETROS//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//
    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    // Start is called before the first frame update
    void Start()
    {
        lifePanel.gameObject.SetActive(false);
        _difficulty = GameMode.easy;
        ShowMaxScore();
        resetButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameState.Equals(GameState.inGame))
        {
            followMouseObject.transform.position = MousePosition();
        }
    }

    /// <summary>
    /// Método que inicia la partida y modifica el estado del juego
    /// </summary>
    public void StartGame(GameMode difficulty = GameMode.easy) 
    {
        _difficulty = difficulty;
        SetDifficulty(difficulty);
        livesCount = lives.Count;
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        _gameState = GameState.inGame;
        UpdateScore(0);
        StartCoroutine(spawnObjects());
        followMouseObject = GameObject.Find("FollowMouse");
    }

    /// <summary>
    /// Settea la dificultad
    /// </summary>
    void SetDifficulty(GameMode difficulty) 
    {
        switch (difficulty) 
        {
            case GameMode.easy:
                lifePanel.gameObject.SetActive(true);
                break;
            case GameMode.normal:
                spawnSeconds /= 2f;
                lifePanel.gameObject.SetActive(true);
                break;
            case GameMode.hard:
                lives.Clear();
                spawnSeconds /= 2f;
                for (int i = 0; i < cantMoreBombs; i++)
                {
                    objects.Add(bomb);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Quita una vida al jugador a menos que no queden mas. En ese caso envia GAME OVER
    /// </summary>
    public void LoseLife() 
    {
        if (livesCount != 0)
        {
            livesCount--;
            Image heartImage = lives[livesCount].GetComponent<Image>();
            Color heartColor = heartImage.color;
            heartColor.a = 0.3f;
            heartImage.color = heartColor;
        }
        else 
        {
            ChangeGameOver();
        }
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
    /// Escribe el texto de la mayor puntuacion
    /// </summary>
    public void ShowMaxScore() 
    {
        int eIntMaxScore = PlayerPrefs.GetInt(eMaxScore);
        eMaxScoreNum.text = eIntMaxScore.ToString();
        int nIntMaxScore = PlayerPrefs.GetInt(nMaxScore);
        nMaxScoreNum.text = nIntMaxScore.ToString();
        int hIntMaxScore = PlayerPrefs.GetInt(hMaxScore);
        hMaxScoreNum.text = hIntMaxScore.ToString();
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
        SetMaxScore();
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
    /// Settea el score maximo
    /// </summary>
    void SetMaxScore() 
    {
        switch (difficulty)
        {
            case GameMode.easy:
                if (score > PlayerPrefs.GetInt(eMaxScore))
                {
                    PlayerPrefs.SetInt(eMaxScore, score);
                }
                break;
            case GameMode.normal:
                if (score > PlayerPrefs.GetInt(nMaxScore))
                {
                    PlayerPrefs.SetInt(nMaxScore, score);
                }
                break;
            case GameMode.hard:
                if (score > PlayerPrefs.GetInt(hMaxScore))
                {
                    PlayerPrefs.SetInt(hMaxScore, score);
                }
                break;
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
