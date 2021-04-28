using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour
{
    private Button button;
    private GameManagerX gameManagerX;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    /* When a button is clicked, call the StartGame() method
     * and pass it the difficulty value (1, 2, 3) from the button 
    */
    void SetDifficulty()
    {
        switch (button.gameObject.name)
        {
            case "Easy Button":
                gameManagerX.StartGame(1);
            break;
            case "Medium Button":
                gameManagerX.StartGame(2);
                break;
            case "Hard Button":
                gameManagerX.StartGame(3);
                break;
        }
    }



}
