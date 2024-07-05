using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class test : MonoBehaviour
{
    // byte (0 ~ 255) - 1바이트
    // sbyte (-128 ~ 127) - 1바이트 
    // short : (-3만 ~ 3만) - 2바이트 / ushort : (0 ~ 6만)
    // int : (-20억 ~ 20억) - 4바이트 / uint : (0 ~ 40억)
    // long : int 상위호환 - 8바이트 / ulong : ~~

    // uint, ushort, ulong 등은 음수가 없고 음수가 빠진 범위만큼 양수 범위가 늘어남

    // 실수 계산시에는 소수점 오차가 반드시 생기는데 형태별로 오차의 폭이 크냐 작냐의 차이가 존재
    // float > double > decimal (float 가장 오차가 크고 decimal은 거의 오차가 없음)
    // float f = 4.000f; (뒤에 f 붙여야함) / decimal d = 4.000m; (뒤에 m 붙여야함) / double은 그냥쓰면됨
    
    // 계산시에는 상위호환과 하위호환은 계산이 가능하지만 하위호환으로 선언된것을 상위호환으로 계산은 불가
    // long l = 0; ing i = 0; 일때 long sum = l + i; 는 가능하지만 int sum = l + i; 는 불가

    // * 캐스트란, 강제로 형태를 변환시키는것 위의 불가한 계산식도 int sum = (int)(l + i); 이런식으로 강제로 결과값을 int형으로 바꾸면 가능

    // string은 "(쌍따옴표 사용, 글자 제한 x)
    // char 는 '(따옴표 사용, 한글자만 저장가능 = 그 글자에 대한 UNICODE를 저장하는것)

    // * 변수 선언시에는 private 이라는 접근지정자가 default 값
    // private : 현재 클래스에서만 사용가능한 변수
    // public : 모든 클래스에서 그 변수 참조가 가능
    // protected : 선언된 클래스를 상속받은 자식 클래스까지만 변수 참조 가능
    int x = 100;
    float floatValue = 10.5f;
    // static 공유자원이란 뜻으로 이 t가 변경되면 선언된 클래스로 만들어진곳의 자원이 다 함께 바뀜 (정적변수)
    public static int t = 5;

    // 함수는 가급적 첫글자 대문자로
    // 함수안에 return 값이 없을땐 void 형태로 사용 / 반환값이 있다면 그 형태로 작성
    void Cast(float parameter = 10.5f) { // 값 자체를 입력해두면 파라미터 안들어왔을때 default 값으로 사용
        (int)(parameter)
    }
    
    // * 비트연산자 &(AND), |(OR), ^(XOR), ~(NOT), <<, >>

    // * do while 문이란, do 부분의 내용을 먼저 실행후 while에서 조건을 보게끔 하는것
    // 그냥 while문은 조건을 보고 그 다음 내용을 실행하기때문에 조건 순서가 좀 다름

    // * 2차원배열 선언
    int[,] array = {{1,2,3,4,5}, {6,7,8,9,10}}
    print(array[0,1]) // 2
    // 3차원배열 선언
    int[,,] array2 = {{{1,2,3,4,5}, {6,7,8,9,10}}, {{11,12,13,14,15}, {16,17,18,19,20}}}
    print(array2[1,1,2]) // 18

    // 기본 배열 (길이 늘림 불가)
    int[] exp = new int[5] {0,1,2,3,4}
    // * 컬렉션 : 리스트, 큐, 스택, 해시테이블, 딕셔너리, 어레이리스트
    // 1. ArrayList (일반 배열과 사용방법은 같고, 길이는 자동적으로 늘어남, 타입도 자유) 
    // >> 자유도가 높기때문에 타입변환을 내부적으로 진행해 로직이 무거움
    ArrayList arrayList = new ArrayList()
    // 2. list (타입을 정해주기때문에 들어올 변수 타입이 정해져있음)
    List<int> list = new List<int>();
    // 3. HashTable (해시테이블은 값을 add할때 key값도 같이 파라미터로 받고 출력시에도 key값으로 찾아야함)
    HashTable hashTable = new HashTable();
    // 4. Dictionary (위의 해시테이블과 같지만 타입을 명시해준다는 점이 추가로 있음)
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    // 5. Queue (선입선출 , FIFO(first in first out)) (타입지정은 해줘도되고 안해줘도됨)
    // 값을 언큐(삽입)후에 디큐(추출)하면 가장 처음 들어간 값이 나오고 index가 하나씩 당겨짐
    Queue<int> queue = new Queue<int>();
    // 6. Stack (후입선출, LIFO(last in first out)) (타입지정은 해줘도되고 안해줘도됨)
    // 값을 push(삽입)후에 pop(추출)하면 가장 나중에 들어간 값이 나옴
    Stack<int> stack = new Stack<int>();

    int power;
    int defence;
    public void SetPower(int a, int b) {power += a+b;}
    public void SetDefence(int value) {defence += value;}
    // * 델리게이트는 함수모음집 같은것으로 한곳으로 넣어서 관리가 가능 (받는 파라미터는 통일필요)
    // 두개의 형태로 선언가능
    public delegate void ChainFunc<T>(T a, T b);
    ChainFunc<int> chain;
    public static event ChainFunc chain2; // 이렇게 선언하면 타 클래스에서 추가빼기 가능 (static 이기때문)

    // action과 func 를 이용하면 델리게이트를 설정하지 않아도 사용가능
    Action<int, int> action;
    Func<int, int, string> func;

    // * 함수의 재정의
    // 함수 선언시에 protected나 public으로 선언후 virtual 이라고 문구를 추가해 가상함수로 만들면
    // 타 클래스에서 사용시에 override 문구를 통해서 함수 재정의 가능
    
    // 부모 클래스에서 abstract 문구를 사용해 함수를 선언하면 그 함수는 추상함수가 되어서
    // 부모 클래스가 아닌 자식 클래스에서 완성해서 사용해야한다.
    // 이때 추상함수를 쓰게되면 그 부모클래스 앞에도 abstract를 붙여 추상클래스로 만들어줘야함

    // * 프로퍼티
    // 정보의 은닉성을 위해 변수는 private으로 설정후 다른곳에서 읽을순 있도록 return 하는 함수만 public으로 만들게될때
    // 변수 하나당 함수를 여러개 만들게되니 작업이 난해해기때문에 프로퍼티로 get, set 기능등을 한꺼번에 넣어준다.
    // 이때 get이나 set 앞에 private을 넣어주기도 가능, 안에서 조건문등이나 다른것도 추가가능
    private int data;
    public int ReadData { get { return data; } private set { data = value; } } // value는 미리 설정되어있는 파라미터 이름
    public int ReadData2 { get; set;} // 간략화로 위처럼 따로 변수선언, 프로퍼티 선언 안해도 가능

    // * 인덱서
    // 타 클래스에서 부모 클래스의 배열값을 참조할때 즉시 그 클래스에서 참조 가능하도록 부모클래스에서 배열 자체를 부모클래스에 종속시키는 방법
    public int[] indexArray = new int[5];
    public int this[int index] { // 이렇게 설정해놓으면 이 클래스[index] 값을 자식클래스에서 접근해 확인해보면 배열값이 나오고 set도 가능
        get { return indexArray[index]; }
        set { indexArray[index] = value; }
    }

    // * 인터페이스
    // 클래스는 상속받을때 하나의 클래스만 상속가능한데 인터페이스는 다중상속이 가능하다.
    // 이때 인터페이스 안에 만든것은 선언만 되어서 상속받은곳에서 완성해줘야함 (이때 override 문구 안써줘도됨)
    // 또한 인터페이스는 변수선언이 불가하고 함수, 프로퍼티, 인덱서, 이벤트 정도만 가능
    // 인터페이스 끼리도 상속가능

    // * 형식매개변수
    // 함수 선언시에 들어올 타입을 유동성있게 받기위하여 형식매개변수로 지정하고 함수 사용하는곳에서 타입을 지정하게끔 한다.
    // T도 제약을 걸수있음 (where) -> struct, class, interface 등등
    void val<T>(T value) where T : struct {
        // 그리고 형식매개변수로 받아서 선언한 변수나 배열은 그 타입을 그대로 따라감
        public T var;
        public T[] array;
        print(value);
    }

    // Start is called before the first frame update
    // 게임 실행시 최초 1회 코드 실행구문
    void Start()
    {
        print(x);
        Cast(floatValue);
        
        print(t++) // 이건 5가 나오고
        print(++t) // 이건 6이 나옴  >> ++이 먼저오냐 나중에 오냐에 따라 순서대로 진행

        // 만들어둔 델리게이트에 함수를 넣거나 빼기 가능
        chain += SetPower;
        chain2 += SetDefence;
        chain += delegate(int a, int b) { print("무명메소드")} // 따로 함수를 만들지 않아도 델리게이트에 함수 추가가능 (무명메소드)
        chain += (int a, int b) => print("람다식") // 무명메소드를 화살표함수처럼 짜서 델리게이트에 추가도 가능 (람다식)
        chain -= SetPower;
        chain(3,5);

        // * func의 사용
        func = (int a, int b) => {int sum = a + b; return sum + "이 리턴되었습니다."}
        func(3, 5)

        // * 형식 매개변수 사용
        val<float>(4.5f);

        // * 예외처리
        int a = 5;
        int b = 0;
        int c;

        try {
            c = a / b; // 0으로 나누려고 하면 DivideByZeroException 에러 발생
        }
        // catch는 여러개 가능
        catch(DivideByZeroException ie) { //DivideByZeroException 에러시 예외처리 / 발생 리턴값은 관례상 ie로 선언
            print(ie);
            b = 1;
            c = a / b; // 원인해결후 재적용
        }
        finally { // 오류가 발생하든 발생하지않든 최종적으로 실행될 마지막 명령어
            print(c); // 마지막에 다시 결과값 도출
        }

        throw new Exception("일부러 오류를 발생시킴"); // throw는 일부러 에러를 발생시킴

        // * 코루틴
        // 병렬처리를 지원하진 않지만 비동기작업을 효율적으로 처리가능하게끔 하는 기능
        // 실행방법은 총 3가지로 나뉨 (아래 참조 - loop a,b,c로 나눠둠)
        // 각각 파라미터 받는 갯수 차이나 과부하 정도의 차이가 있음
        Coroutine myCoroutine1; // 스탑 코루틴을 위한 변수 선언 (예시이기에 여기서 선언하지만 원래는 상단에서 선언)
        IEnumerator myCoroutine3;
        myCoroutine3 = LoopC();

        myCoroutine1 = StartCoroutine(LoopA()); //StartCoroutine안에 실행할 코루틴 함수를 넣어줌
        myCoroutine2 = StartCoroutine("LoopB");
        StartCoroutine(myCoroutine3)
        StartCoroutine(stop());
    }

    IEnumerator LoopA() { // IEnumerator 를 사용, 코루틴으로서(병령) 돌아가게됨
        for (int i = 0; i < 100; i++) {
            print("i의 값 = " + i);
            // 코루틴 사용시 반드시 yield return (기다리는시간 설정 필요)
            yield return new WaitForSeconds(1f); // 여기서는 1초 기다리게끔 되어있음
        }
    }

    IEnumerator LoopB() {
        for (int x = 0; x < 100; x++) {
            print("x의 값 = " + x);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator LoopC() {
        for (int c = 0; c < 100; c++) {
            print("c의 값 = " + c);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Stop() {
        yield return new WaitForSeconds(2f); // 2초때 LoopA 코루틴 종료
        StopCoroutine(myCoroutine1)
        yield return new WaitForSeconds(2f); // 또 2초 지나서 LoopB 코루틴 종료
        StopCoroutine("LoopB")
        yield return new WaitForSeconds(2f); // 또 2초 지나서 LoopC 코루틴 종료
        StopCoroutine(myCoroutine3)
        yield return new WaitForSeconds(2f);
        StopAllCoroutines(); // 모든 코루틴 종료 함수도 존재 (단, 지금 보고있는 Stop 코루틴도 종료됨)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
