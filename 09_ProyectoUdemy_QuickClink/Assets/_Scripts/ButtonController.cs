using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDificulty);
    }


    /// <summary>
    /// Llama al metodo de GameManager para iniciar el juego
    /// </summary>
    void SetDificulty() 
    {
        switch (gameObject.tag)
        {
            case "Easy":
                gameManager.StartGame(GameManager.GameMode.easy);
                break;
            case "Normal":
                gameManager.StartGame(GameManager.GameMode.normal);
                break;
            case "Hard":
                gameManager.StartGame(GameManager.GameMode.hard);
                break;
            default:
                gameManager.StartGame();
                break;
        }
    }
}
