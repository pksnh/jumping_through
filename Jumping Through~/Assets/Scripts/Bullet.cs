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

        // ���� ��ġ�� �������� �������� -width �̻� �̵������� ��ġ�� ����
        if (transform.position.x >= 20f)
        {
            Reposition();
            //Instantiate �� ���� ����: ���ο� ��ü ���� �� �ð� �ɸ�->���� �÷��� ���� ���� �߻� ����
        }

    }

    // ��ġ�� �����ϴ� �޼���
    private void Reposition()
    {
        Vector2 offset = new Vector2(0, -25);
        transform.position = offset;
    }
}
