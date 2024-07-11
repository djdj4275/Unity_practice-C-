using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    WaitForFixedUpdate wait;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate() {
        // enemy가 죽었거나, 피격 애니메이션일때는 자동추적 이동 안함
        // GetCurrentAnimatorStateInfo(0) 는 애니메이션의 layer 번호 (0은 base layer)
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position; // 플레이어의 포지션에서 적군의 포지션값 뺌
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 갈 위치

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리적으로 밀리지 않도록 velocity값 0으로
    }

    private void LateUpdate() {
        if (isLive) {
            spriter.flipX = target.position.x < rigid.position.x;
        }
    }

    private void OnEnable() { // 스크립트가 활성화 될때, 호출되는 이벤트 함수
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

        // 재활용될시에 다시 죽었던 옵션 되살려야함
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    public void Init(SpawnData data) {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Bullet") || !isLive) return;

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack"); // StartCoroutine(KnockBack()) 이렇게도 가능

        if (health > 0) {
            // 아직 살아있음, 히트액션
            anim.SetTrigger("Hit");
        } else {
            // 사망
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack() {
        yield return wait; // 다음 하나의 물리 프레임을 딜레이 

        // 플레이어의 반대방향으로 힘을 부여해서 넉백되도록
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // Impulse(즉발)
    }

    void Dead() {
        gameObject.SetActive(false);
    }
}
