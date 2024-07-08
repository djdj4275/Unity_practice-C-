using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI textGoal;

    public int goal;

    [SerializeField]
    private Color green;
    [SerializeField]
    private Color red;

    [SerializeField]
    private GameObject btnRetry;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textGoal.SetText(goal.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseGoal() {
        goal -= 1;
        textGoal.SetText(goal.ToString());

        if (goal <= 0) {
            SetGameOver(true);
        }
    }

    public void SetGameOver(bool success) {
        if (!isGameOver) {
            isGameOver = true;

            Camera.main.backgroundColor = success ? green : red;

            Invoke("ShowRetryButton", 1f);
        }
    }

    void ShowRetryButton() {
        btnRetry.SetActive(true);
    }

    public void Retry() {
        SceneManager.LoadScene("SampleScene");
    }
}
