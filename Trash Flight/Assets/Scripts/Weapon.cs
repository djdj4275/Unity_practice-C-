using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    public float damage = 1f; // 다른곳에서 데미지 계산을 위해 public으로

    // Start is called before the first frame update
    void Start() // start는 처음 오브젝트가 생성될때 한번, 게임 객체를 비활성화 했다가 다시 활성화 할때 한번 호출됨
    {
        Destroy(gameObject, 1f); // 생성되고 난후 1초후에 현 객체 destroy
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime; // Time.deltaTime은 성능이 다른컴퓨터에서도 같은 속도를 유지하기위함
    }
}
