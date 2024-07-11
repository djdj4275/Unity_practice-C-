using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 이 둘은 1:1 관계
    // 프리펩들을 보관할 변수 필요
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들이 필요
    List<GameObject>[] pools;

    private void Awake() { // 각각의 변수 초기화
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++) { // 배열이기 때문의 안에 요소들도 각각 초기화
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) {
        GameObject select = null;

        // 선택한 풀의 놀고 있는 (비활성화 된) 게임오브젝트 접근
        // 발견하면 select 변수에 할당
        foreach (GameObject item in pools[index]) {
            if (!item.activeSelf) {
                select = item;
                select.SetActive(true); // 활성화
                break;
            }
        }

        // 몾찾으면 새롭게 생성해서 select 변수에 할당
        if (select == null) {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
