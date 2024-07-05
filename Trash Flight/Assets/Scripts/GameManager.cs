using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;

    [HideInInspector] // public 변수이지만 inspector 창에서 보이지 않게됨
    public bool isGameOver = false;

    // 싱글턴 디자인 방식
    void Awake() { // Awake는 Start보다 한단계 더 빠르게 호출되는 메소드
        if (instance == null) {
            instance = this; // 게임매니저 초기화
        }
    }

    public void IncreaseCoin() {
        coin += 1;
        text.SetText(coin.ToString());

        if (coin % 30 == 0) { // 30, 60, 90...
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver() {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("showGameOverPanel", 0.5f); // 0.5초 기다린뒤에 함수 실행
    }

    void showGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
