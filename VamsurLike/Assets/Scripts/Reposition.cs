using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Collider2D coll;

    private void Awake() {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Area"))
        return;

        Vector3 playerPos = GameManager.instance.player.transform.position; // 플레이어 위치
        Vector3 myPos = transform.position; // 타일맵 위치

        float diffX = Mathf.Abs(playerPos.x - myPos.x); // 절댓값으로 계산
        float diffY = Mathf.Abs(playerPos.y - myPos.y); 

        // 플레이어 위치값 input system으로 받았음
        Vector3 playerDir = GameManager.instance.player.inputVec;
        // 노멀라이징 때문에 값이 1보다 작을수있음 (대각선 이동 때문)
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag) {
            case "Ground" :
                if (diffX > diffY) { // 유저가 맵밖으로 수평이동시
                    transform.Translate(Vector3.right * dirX * 40);
                } else if (diffX < diffY) { // 유저가 맵밖으로 수직이동시
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy" :
                if (coll.enabled) {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f) , 0f)); // 적이 지형 경계밖을 나가게 되면 좀 더 위협적일수 있도록 플레이어 반대편에서 나오도록함
                }
                break;
        }
    }
}
