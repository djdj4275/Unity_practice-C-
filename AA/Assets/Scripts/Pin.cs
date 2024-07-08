using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private bool isPinned = false;
    private bool isLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() // 기본 Update를 쓰게되면 주기가 매번 달라지기때문에 게임 핀이 꽂힐때 어떤건 깊게 꽂히고 어떤건 얕게 꽂힌듯이 보이기때문에 주기를 고정하기위해 FixedUpdate 사용
    {
        if (!isPinned && isLaunched) {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isPinned = true;
        if (other.gameObject.tag == "TargetCircle") {
            // GameObject childObject = transform.Find("Square").gameObject; // 핀의 하위인 Square 객체 찾아옴
            GameObject childObject = transform.GetChild(0).gameObject; // 핀의 하위인 자식중 첫번째 (위 코드랑 골라서 사용)
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true; // 자식 활성화 (막대 보이게끔 하기)
            print(childSprite.enabled);

            transform.SetParent(other.gameObject.transform); // 타겟원과 만나면 그 타겟원 하위로 속하게됨

            GameManager.instance.DecreaseGoal();
        } else if (other.gameObject.tag == "Pin") {
            GameManager.instance.SetGameOver(false);
        }
    }

    public void Launch() {
        isLaunched = true;
    }
}
