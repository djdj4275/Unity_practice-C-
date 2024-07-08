using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드의 상하좌우 값을 받음 (Axis 값은 -1 ~ 1 값까지만)
        // GetAxisRaw 는 GetAxis보다 더 명확한 컨트롤 구현가능 (미끄러짐 현상 방지)
        // inputVec.x = Input.GetAxisRaw("Horizontal");
        // inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        // 캐릭 움직이기
        // 1. 힘을 준다
        // rigid.AddForce(inputVec);
        // 2. 속도 제어
        // rigid.velocity = inputVec;
        // 3. 위치 이동
        // Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; // 플레이어가 대각선 이동시에는 1이 아닌 루트2로 이동하기때문에 그 또한 1로 수렴하게끔 노멀라이징 + speed 변동위한 변수수치 + fixedUpdate 물리프레임 하나가 소비한 시간(환경마다 다르기때문에 통일을 위함)
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime; // Input system 사용 옵션으로 노멀라이즈 하기때문에 뺌
        rigid.MovePosition(rigid.position + nextVec); // 위치 이동이기 때문에 현재 위치도 더해줘야함.
    }

    // 키보드 입력값을 Input.GetAxisRaw이 아닌 새로나온 InputSystem의 방식으로 받음
    // 함수 앞에 On을 붙이면 사용가능 (package manager에 input system 설치후 input action 컴포넌트 만들어 추가해서 사용)
    // 이 함수들은 자동완성이 안됨.
    void OnMove(InputValue value) {
        inputVec = value.Get<Vector2>(); // 또한 Input system 추가시에 자체옵션에서 노멀라이즈 옵션을 추가하기때문에 여기서 안해도됨.
    }

    private void LateUpdate() { // 프레임이 종료되기전 실행되는 생명주기 함수
        anim.SetFloat("Speed", inputVec.magnitude); // 애니메이션 안에 있는 Speed 파라미터값을 플레이어가 이동하는 vector 값을 순수 길이값인 magnitude 속성으로 넣어줌 => 대각선도 똑같이 1로 들어가게끔

        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0; // 비교연산자 바로 넣어도 됨
        }
    }
}
