using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;
    
    public GameObject gameOverPanel; 
    private bool isGameOver = false;

    public Action OnGameOver; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        OnGameOver += ShowGameOverUI;
    }

    void Start()
    {
        gameOverPanel.SetActive(false); // 시작 시 UI 꺼두기
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void ShowGameOverUI()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // 게임 정지
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // 게임 재개
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    private void OnDestroy()
    {
        OnGameOver -= ShowGameOverUI;
    }
}