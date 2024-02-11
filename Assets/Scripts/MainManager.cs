using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] Text bestScoreText;
    [SerializeField] InputField nameInputField;

    public Text ScoreText;
    public GameObject GameOverText;
    [SerializeField] GameObject pressSpaceText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    string fileName = "savedata.json";
    string path;

    [System.Serializable]
    class BestScore
    {
        public string name = "";
        public int bestScore = 0;
    }
    BestScore bestScore;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = new BestScore();
        path = $"{Application.persistentDataPath}/{fileName}";
        LoadScore();
        if (bestScore.name != "")
        {
            bestScoreText.text = $"Best Score : {bestScore.name} : {bestScore.bestScore}";
            bestScoreText.gameObject.SetActive(true);
        }

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !nameInputField.IsActive())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (m_Points > bestScore.bestScore)
        {
            bestScore.bestScore = m_Points;
            bestScoreText.text = $"Best Score : {bestScore.bestScore}";
            
            if (!bestScoreText.IsActive())
                bestScoreText.gameObject.SetActive(true);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (bestScore.bestScore > m_Points)
        {
            nameInputField.gameObject.SetActive(true);
        }
        else
        {
            pressSpaceText.SetActive(true);
        }
    }

    private void SaveScore()
    {
        string json = JsonUtility.ToJson(bestScore);

        File.WriteAllText(path, json);
    }

    private void LoadScore()
    {
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            bestScore = JsonUtility.FromJson<BestScore>(json);
        }
    }

    public void OnNameInputValueChanged()
    {
        bestScore.name = nameInputField.text;
        bestScoreText.text = $"Best Score : {bestScore.name} : {bestScore.bestScore}";
    }

    public void OnNameInputEndEdit()
    {
        bestScore.name = nameInputField.text;
        bestScoreText.text = $"Best Score : {bestScore.name} : {bestScore.bestScore}";
        SaveScore();
        nameInputField.gameObject.SetActive(false);

        pressSpaceText.SetActive(true);
    }
}
