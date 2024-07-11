using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 스캔할 범위
    public LayerMask targetLayer; // 스캔할 레이어
    public RaycastHit2D[] targets; // 스캔 결과 배열
    public Transform nearestTarget; // 가장 가까운 목표

    private void FixedUpdate() {
        // 원형의 캐스트를 쏘고 모든 결과를 반환 (캐스팅 시작위치, 원의 반지름, 케스팅 방향, 캐스팅 길이, 대상 레이어)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest() {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets) {
            Vector3 myPos = transform.position; // 플레이어의 위치
            Vector3 targetPos = target.transform.position; // 임의의 타겟 위치
            
            float curDiff = Vector3.Distance(myPos, targetPos); // 위치간 거리

            // 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 교체 (가장 거리가 가까운 타겟값 찾기)
            if (curDiff < diff) {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
