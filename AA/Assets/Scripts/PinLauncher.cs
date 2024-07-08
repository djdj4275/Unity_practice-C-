using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pinObject;

    private Pin currPin; // 발사준비된 pin

    // Start is called before the first frame update
    void Start()
    {
        PreparePin();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currPin != null && !GameManager.instance.isGameOver) {// 왼쪽 마우스 버튼(0) 클릭했을때 pin이 준비가 되어있고, 게임오버가 아니라면
            currPin.Launch();
            currPin = null; // 발사된 핀은 이제 제어할 필요가 없기에 다음핀을 넣기위해 null로 초기화
            Invoke("PreparePin", 0.1f);
        }
    }

    void PreparePin() {
        if (!GameManager.instance.isGameOver) { // 게임오버가 아니라면 핀 준비함 (성공하든 실패하든 게임은 오버됨)
            GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
            currPin = pin.GetComponent<Pin>();
        }
    }
}
