using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 장애물 오브젝트들, Hierachy 창에 이미 만들어져 있었던 오브젝트->이미 메모리 공간 존재
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable()
    {
        // 발판을 리셋하는 처리
        stepped = false;

        /*for(int i=0;i<obstacles.Length;i++) // Length: 배열의 길이
        {
            if(Random.Range(0,3) == 0) // Random.Range(0,3): 0, 1, 2 중 하나 출력
            {
                obstacles[i].SetActive(true);                
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }*/

    }

    void OnCollisionEnter2D(Collision2D collision) // 부딛힌 정보는 collision으로 들어 옴(platform: Player collision 들어옴 / Player: Platform collison 들어옴)
    {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if(collision.collider.tag=="Player"&&!stepped)
        {
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}