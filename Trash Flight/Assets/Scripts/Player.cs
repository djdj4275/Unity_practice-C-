using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // 이 설정을 하면 여기서 선언한 클래스안의 변수를 unity툴에서 확인가능
    private float moveSpeed; // 유니티 툴에서 변동시킬 수치

    [SerializeField]
    private GameObject[] weapons; // 플레이어객체 안에서 생성해서 사용될 오브젝트 넣을공간 만들기 (prefab에 있는 무기넣을 공간)

    private int weaponIndex = 0; // 사용할 무기

    [SerializeField]
    private Transform shootTransform; // scene에서 정해둔 무기가 생성될 위치값 받을 변수

    [SerializeField]
    private float ShootInterval = 0.1f; // 무기가 생성될 텀
    private float lastShotTime = 0f; // 마지막 무기가 생성된 시간

    // Update is called once per frame
    void Update()
    {
        // 1. 키보드로 유닛 움직이기
        // float horizontalInput = Input.GetAxisRaw("Horizontal"); // 키보드 방향키 좌/우 값 받기
        // float verticalInput = Input.GetAxisRaw("Vertical"); // 키보드 방향키 상/하 값 받기

        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f); // 이동할 값

        // transform.position += moveTo * moveSpeed * Time.deltaTime; // 플레이어 객체 움직이게 하기

        // 2. 키보드로 유닛 움직이기2
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if (Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // } else if (Input.GetKey(KeyCode.RightArrow)) {
        //     transform.position += moveTo;
        // }

        // 3.마우스로 유닛 움직이기
        // Debug.Log(Input.mousePosition); // 로그 찍기
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스의 좌표값을 우리가 scene에서 보는 좌표값으로 변환
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f); // 마우스는 옆의 벽과 상관없이 충돌처리가 안되기때문에 좌/우 x좌표의 최소,최대값을 정해줘야함
        transform.position = new Vector3(toX, transform.position.y, 0); // 원래 플레이의 y값은 유지하고 x값만 마우스값 따라감 (z는 2d게임이니 0)

        Shoot();
    }

    void Shoot() {
        // 정해둔 ShootInterval 시간보다 게임이 흐른시간에 마지막으로 weapon을 생성한 시간이 커지면 (ShootInterval이 지나면)
        // weapon을 생성하고 마지막 생성된 시간 lastShotTime을 현재시간으로 초기화해줌
        if (Time.time - lastShotTime > ShootInterval) { // Time.time은 게임이 시작된 이후로 현재까지 흐른 시간을 말함
            // shoot함수가 실행되면 prefab에 넣어둔 weapon을 이미지를 생성하고 포지션값은 scene에서 설정해둔 값으로 생성되며, 회전정도는 기본으로 넣어둠(없음)
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }  
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") { // 적이랑 부딪히면 플레이어 죽음
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade() {
        weaponIndex += 1;

        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
