using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.GraphicsBuffer;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // 생성할 발판의 원본 프리팹

    private int count = 5; // 생성할 총알의 개수
    private float timeBetSpawn = 0.5f; // 다음 배치까지의 시간 간격

    private GameObject[] bullets; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -30); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점

    private Vector2 shotposition;
    private PlayerController pc;

    private AudioSource audio;
    public AudioClip a;

    // Start is called before the first frame update
    void Start()
    {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        bullets = new GameObject[count]; // 아직 hieracy 창에 실체 없음->메모리 공간 존재x->new GameObject[count]로 platforms에 메모리 공간 할당

        for (int i = 0; i < count; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, poolPosition, Quaternion.identity);
        } 

        pc=FindObjectOfType<PlayerController>();
        
        lastSpawnTime = 0f;

        audio=GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        shotposition = pc.GetComponentInChildren<Transform>().position;

        if (GameManager.instance.isGameover)
        {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn) //Time.time: 프로그램 실행 후 지난 시간(초 단위) 가리킴(현재 시간처럼 사용 가능)
        {
            lastSpawnTime = Time.time;

            bullets[currentIndex].transform.position = shotposition;
            audio.PlayOneShot(a);
            
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0; // currentIndex가 3 이상이 되면 0으로 초기화(0->1->2 반복)
            }
        }
    }
}
