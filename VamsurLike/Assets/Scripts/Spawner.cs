using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // 제일낮은값 정하기
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); // 소수점 버림

        if (timer > spawnData[level].spawnTime) {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn() {
        GameObject enemy = GameManager.instance.Pool.Get(0); // 적 소환;
        // enemy의 생성지점은 랜덤 (1부터 시작하는 이유는 GetComponentsInChildren가 자기 자신도 포함해서 들고오기 때문)
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


// 소환 데이터를 담당하는 클래스 선언
// inspector 창에서 볼수있게끔 직렬화
[System.Serializable]
public class SpawnData {
    public float spawnTime;

    // enemy에서 사용할 3가지 값
    public int spriteType;
    public int health;
    public float speed;
}