using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // static으로 선언된 자원은 바로 메모리에 올라가며,
    // public으로 사용되었지만 inspector창에 나타나지 않음.
    public static GameManager instance; 

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f; // 20초

    [Header("# Player Info")]
    public int level; // 레벨
    public int kill; // 킬수
    public int exp; // 경험치
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager Pool;
    public Player player;

    private void Awake() {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
        }
    }

    public void GetExp() {
        exp++;

        if (exp == nextExp[level]) {
            level++;
            exp = 0;
        }
    }
}
