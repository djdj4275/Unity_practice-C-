using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies; // tool에서 prefabs로 넣을 enemy 객체들

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.1f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f); // 3초 기다린후 코루틴 로직 시작

        int enemyIndex = 0;
        int spawnCount = 0;
        float moveSpeed = 5f;

        while (true) { // 무한루프
            foreach (float posX in arrPosX) {
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;

            if (spawnCount % 10 == 0) { // 10, 20, 30... // 적이 10개 단위로 스폰될때마다 다음단계 적을 등장시키고 속도가 빨라짐
                enemyIndex += 1;
                moveSpeed += 1;
            }

             yield return new WaitForSeconds(spawnInterval); // 위의 spawnInterval 만큼 기다렸다가 다시 반복문 내용 시작
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, 0);

        if (Random.Range(0, 5) == 0) { // 0~4 랜덤값 뽑아서 그게 0이면 (20%확률) 다음 단계의 몹이 등장
            index += 1;
        }

        if (index > enemies.Length) { // 위에서 내 enemy prefabs 길이보다 index가 올라가면 다시 줄여줌
            index -= 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity); // 새로운 적 객체를 생성
        Enemy enemy = enemyObject.GetComponent<Enemy>(); // 만든 적 객체 내부의 컴포넌트중 Enemy 컴포넌트 들고옴
        enemy.SetMoveSpeed(moveSpeed); // 그안에 public으로 선언되어있는 함수 실행
    }
}
