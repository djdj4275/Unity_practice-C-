using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    private int coin = 0;

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
}
