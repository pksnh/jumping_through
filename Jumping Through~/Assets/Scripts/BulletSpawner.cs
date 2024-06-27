using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.GraphicsBuffer;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ������ ���� ������

    private int count = 5; // ������ �Ѿ��� ����
    private float timeBetSpawn = 0.5f; // ���� ��ġ������ �ð� ����

    private GameObject[] bullets; // �̸� ������ ���ǵ�
    private int currentIndex = 0; // ����� ���� ������ ����

    private Vector2 poolPosition = new Vector2(0, -30); // �ʹݿ� ������ ���ǵ��� ȭ�� �ۿ� ���ܵ� ��ġ
    private float lastSpawnTime; // ������ ��ġ ����

    private Vector2 shotposition;
    private PlayerController pc;

    private AudioSource audio;
    public AudioClip a;

    // Start is called before the first frame update
    void Start()
    {
        // �������� �ʱ�ȭ�ϰ� ����� ���ǵ��� �̸� ����
        bullets = new GameObject[count]; // ���� hieracy â�� ��ü ����->�޸� ���� ����x->new GameObject[count]�� platforms�� �޸� ���� �Ҵ�

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

        if (Time.time >= lastSpawnTime + timeBetSpawn) //Time.time: ���α׷� ���� �� ���� �ð�(�� ����) ����Ŵ(���� �ð�ó�� ��� ����)
        {
            lastSpawnTime = Time.time;

            bullets[currentIndex].transform.position = shotposition;
            audio.PlayOneShot(a);
            
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0; // currentIndex�� 3 �̻��� �Ǹ� 0���� �ʱ�ȭ(0->1->2 �ݺ�)
            }
        }
    }
}
