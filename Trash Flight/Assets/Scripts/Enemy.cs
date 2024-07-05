using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f; // 화면밖 y값 (destroy 하기위함)

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) { // 다른쪽 클래스에서 쓸수있게 public
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        
        if (transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    // 충돌감지만 할 경우에는 객체의 colider옵션에서 trigger를 체크하면 물리법칙은 적용되진않지만 충돌감지는 가능
    // 물리법칙 적용해서 감지할때에는 OnCollisionEnter2D 함수로 적용
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;

            Destroy(other.gameObject); // 부딪힌 미사일은 바로 부수고

            if (hp <= 0) { // 적군 hp가 0이되면 적군도 부숨
                if (gameObject.tag == "Boss") {
                    GameManager.instance.SetGameOver();
                }
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            }
        }
    }
}
