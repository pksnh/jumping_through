using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    private float bulletspeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead")
        {
            Reposition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletRigidbody==null)
        {
            return;
        }

        if (!GameManager.instance.isGameover)
        {
            transform.Translate(Vector2.right * bulletspeed * Time.deltaTime);
        }

        // 현재 위치가 원점에서 왼쪽으로 -width 이상 이동했을때 위치를 리셋
        if (transform.position.x >= 20f)
        {
            Reposition();
            //Instantiate 안 쓰는 이유: 새로운 개체 생성 시 시간 걸림->게임 플레이 도중 끊김 발생 가능
        }

    }

    // 위치를 리셋하는 메서드
    private void Reposition()
    {
        Vector2 offset = new Vector2(0, -25);
        transform.position = offset;
    }
}
