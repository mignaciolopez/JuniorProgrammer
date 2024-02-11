using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //GUI
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject hud;
    [SerializeField] Button playButton;
    [HideInInspector] public Text scoreText;

    int score;

    [SerializeField] GameObject spherePrefab;

    [SerializeField] float spawnRate = 10.0f;

    [HideInInspector] public bool isGameActive = false;

    

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        isGameActive = false;
        hud.SetActive(false);
        //titleScreen.SetActive(true);

        //To be removed
        StartCoroutine(WaitAndRestart(3f));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        spawnRate /= 1;
        Cursor.lockState = CursorLockMode.Locked;
        hud.SetActive(true);
        titleScreen.SetActive(false);
        score = 0;
        UpdateScore(0);
        isGameActive = true;
        StartCoroutine(SpawnSphere());
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score >= 10)
            GameOver();
    }

    IEnumerator WaitAndRestart(float time)
    {
        yield return new WaitForSeconds(time);
        RestartGame();
    }

    IEnumerator SpawnSphere()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(spherePrefab, RandomPosition(), spherePrefab.transform.rotation);
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 randomPos = Vector3.zero;

        randomPos.x = Random.Range(-50f, 50f);
        randomPos.y = Random.Range(15f, 20f);
        randomPos.z = Random.Range(-50f, 50f);

        return randomPos;
    }
}
