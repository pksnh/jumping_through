//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab; // ������ ������ ���� ������

    private int count = 6; // ������ ���� ����
    private float timeBetSpawn = 1f; // ���� ��ġ������ �ð� ����

    private GameObject[] enemys; // �̸� ������ ����
    private int currentIndex = 0; // ����� ���� ���� ����
    //private int enemyPrefabmin = 0;
    //private int enemyPrefabmax = 3;
    private int enemyPrefabindex;

    private Vector2 poolPosition = new Vector2(0, -45); // �ʹݿ� ������ ���ǵ��� ȭ�� �ۿ� ���ܵ� ��ġ
    private float lastSpawnTime; // ������ ��ġ ����

    public float timeBetSpawnMin = 1.25f; // ���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMax = 2.25f; // ���� ��ġ������ �ð� ���� �ִ�

    public float yMin = -3.5f; // ��ġ�� ��ġ�� �ּ� y��
    public float yMax = 3.5f; // ��ġ�� ��ġ�� �ִ� y��
    private float xPos = 20f; // ��ġ�� ��ġ�� x ��


    // Start is called before the first frame update
    void Start()
    {
        // �������� �ʱ�ȭ�ϰ� ����� ���ǵ��� �̸� ����
        enemys = new GameObject[count]; // ���� hieracy â�� ��ü ����->�޸� ���� ����x->new GameObject[count]�� platforms�� �޸� ���� �Ҵ�

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
        // ������ ���ư��� �ֱ������� ������ ��ġ
        if (GameManager.instance.isGameover)
        {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn) //Time.time: ���α׷� ���� �� ���� �ð�(�� ����) ����Ŵ(���� �ð�ó�� ��� ����)
        {
            lastSpawnTime = Time.time;

            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);

            enemys[currentIndex].transform.position = new Vector2(xPos, yPos);
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0; // currentIndex�� 10 �̻��� �Ǹ� 0���� �ʱ�ȭ(0->1->2 �ݺ�)
            }
        }
    }
}
