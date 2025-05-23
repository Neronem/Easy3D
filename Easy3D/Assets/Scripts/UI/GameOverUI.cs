using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance; // 싱글톤
    
    public GameObject gameOverPanel; // 게임 오버 UI 패널 
    public bool isGameOver = false; // 현재 게임 오버 상태 여부

    // 게임 오버 이벤트
    public Action OnGameOver;

    private void Awake()
    {
        // 싱글톤 
        if (instance == null)
        {
            instance = this;
        }

        OnGameOver += ShowGameOverUI; // 이벤트에 UI 표시 메서드 등록
    }

    void Start()
    {
        gameOverPanel.SetActive(false); // 게임 시작 시 게임 오버 UI는 꺼져 있어야 함
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R)) // 게임 오버 상태일 때, R 키를 누르면 재시작
        {
            RestartGame();
        }
    }

    // 게임 오버 UI 보여주기
    void ShowGameOverUI()
    {
        isGameOver = true;                 // 게임 오버 상태로 전환
        gameOverPanel.SetActive(true);     // UI 패널 켜기
        Time.timeScale = 0f;               // 게임 일시정지
    }

    // 게임 재시작
    void RestartGame()
    {
        Time.timeScale = 1f; // 시간 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 재로드
    }

    private void OnDestroy() // 오브젝트 파괴 시
    {
        OnGameOver -= ShowGameOverUI; // 이벤트 등록 해제 
    }
}