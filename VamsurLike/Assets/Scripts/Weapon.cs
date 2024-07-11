using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기 id
    public int prefabId; // 프리펩 id
    public float damage; // 데미지
    public int count; // 개수
    public float speed; // 속도

    private float timer; // 타이머

    Player player;

    private void Awake() {
        player = GetComponentInParent<Player>();
    }

    public void Init() {
        switch (id) {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    void Batch() {
        for (int i = 0; i < count; i++) {
            Transform bullet;

            // 배치 새롭게 할때에 먼저 배치된 무기가 있다면 그걸 활용하고 아니라면 풀링에서 가져오기
            if (i < transform.childCount) {
                bullet = transform.GetChild(i);
            } else {
                bullet = GameManager.instance.Pool.Get(prefabId).transform;
                bullet.parent = transform; // 부모 셋팅도 이미 생성된 무기라면 되어있기에 아닌경우에만 새로 배당
            }

            bullet.localPosition = Vector3.zero; // 초기 값으로 초기화 먼저 (새로 배치될 경우 대비)
            bullet.localRotation = Quaternion.identity; // 초기화

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1은 관통 유지
        }
    }

    public void LevelUp(float damage, int count) {
        this.damage = damage;
        this.count += count;

        if (id == 0) {
            Batch();
        }
    }

    private void Fire() {
        if (player.scanner.nearestTarget) {
            Vector3 targetPos = player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.instance.Pool.Get(prefabId).transform;
            bullet.position = transform.position; // 위치 결정
            // 지정된 축 중심으로 목표 향해 회전하는 함수
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 회전 설정
            bullet.GetComponent<Bullet>().Init(damage, count, dir); // 불렛에게 전달
        }
    }

    private void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }
}
