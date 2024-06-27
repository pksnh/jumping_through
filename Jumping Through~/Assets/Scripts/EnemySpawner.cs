//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab; // 생성할 발판의 원본 프리팹

    private int count = 6; // 생성할 적의 개수
    private float timeBetSpawn = 1f; // 다음 배치까지의 시간 간격

    private GameObject[] enemys; // 미리 생성한 적들
    private int currentIndex = 0; // 사용할 현재 적의 순번
    //private int enemyPrefabmin = 0;
    //private int enemyPrefabmax = 3;
    private int enemyPrefabindex;

    private Vector2 poolPosition = new Vector2(0, -45); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 3.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값


    // Start is called before the first frame update
    void Start()
    {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        enemys = new GameObject[count]; // 아직 hieracy 창에 실체 없음->메모리 공간 존재x->new GameObject[count]로 platforms에 메모리 공간 할당

        for (int i = 0; i < count; i++)
        {
            enemyPrefabindex = Random.Range(0,3);
            enemys[i] = Instantiate(enemyPrefab[enemyPrefabindex], poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if (GameManager.instance.isGameover)
        {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn) //Time.time: 프로그램 실행 후 지난 시간(초 단위) 가리킴(현재 시간처럼 사용 가능)
        {
            lastSpawnTime = Time.time;

            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);

            enemys[currentIndex].transform.position = new Vector2(xPos, yPos);
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0; // currentIndex가 10 이상이 되면 0으로 초기화(0->1->2 반복)
            }
        }
    }
}
