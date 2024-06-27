using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경의 가로 길이
    
    private void Awake() {
        // 가로 길이를 측정하는 처리
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        width = bc.size.x;  //size: 프로퍼티(==멤버변수, +함수 기능 곁들여 편리함 증가)

        /*GameManager instance = new GameManager();
        instance.AddScore(10);*/ // 15번 줄과 동일한 내용

        //GameManager.instance.AddScore(0); // 싱글턴: Gamemanager 오브젝트가 없으면 instance == null => instacne 자동 생성(코드 상)
                                          // 기존 방법: GameManager 오브젝트가 없으면 instance == null => instance = new GameManager();로 GameManager 만듦
                                          // static 함수: 내부에 static 변수만 있어야만 클래스명.함수명(); 가능
    }

    private void Update() {
        // 현재 위치가 원점에서 왼쪽으로 -width 이상 이동했을때 위치를 리셋
        if (transform.position.x <= -width)
        {
            Reposition();
            //Instantiate 안 쓰는 이유: 새로운 개체 생성 시 시간 걸림->게임 플레이 도중 끊김 발생 가능
        }
    }

    // 위치를 리셋하는 메서드
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2) transform.position + offset;
    }
}