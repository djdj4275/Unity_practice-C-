using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float damage; // 데미지
    public int per; // 관통력

    Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir) {
        this.damage = damage;
        this.per = per;

        if (per > -1) {
            rigid.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy") || per == -1) return;

        per--;

        if (per == -1) {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false); // destroy 하지 않는 이유은 poolManager로 관리되며 재활용할거기 때문
        }
    }
}
