using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ActivePlayerText;
    public Text ActiveScoreText;
    private Text BestScoreText;
    public Text BestScoreNameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        MenuManager.Instance.LoadPlayerName();
        ActivePlayer();

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
            HighScore();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void ActivePlayer()
    {
        BestScoreNameText.text = $"{MenuManager.Instance.bestScorePlayerName} {MenuManager.Instance.bestScore}";
        ActivePlayerText.text = $"Player :{MenuManager.Instance.activePlayerName}";
    }


    void AddPoint(int point)
    {
        m_Points += point;
        ActiveScoreText.text = $"Score : {m_Points}";
    }


    void HighScore()
    {
        if (m_Points > MenuManager.Instance.bestScore)
        {
            MenuManager.Instance.bestScore = m_Points;
            MenuManager.Instance.bestScorePlayerName = MenuManager.Instance.activePlayerName;
            Debug.Log($"New High Score: {MenuManager.Instance.bestScore} by {MenuManager.Instance.bestScorePlayerName}");
            MenuManager.Instance.SavePlayerName();
            BestScoreNameText.text = $"{MenuManager.Instance.bestScorePlayerName} {MenuManager.Instance.bestScore}";
        }
        else
        {
            MenuManager.Instance.LoadPlayerName();
        }
    }



    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
