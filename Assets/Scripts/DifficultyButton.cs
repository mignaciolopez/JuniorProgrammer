using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDiffculty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDiffculty()
    {
        Debug.Log(button.gameObject.name + " was clicked.");
        gameManager.StartGame(difficulty);
    }
}
