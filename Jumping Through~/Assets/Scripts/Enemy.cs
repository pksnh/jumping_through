using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    private Collider2D enemyCollider2d;
    public float enemyspeed;
    private Animator animator;
    private float t;
    private bool dead;
    private AudioSource audio;
    public AudioClip a;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider2d=GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
        //dead = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(animator == null)
        {
            return;
        }

        if (other.tag == "bullet")
        {
            dead= true;
            animator.SetBool("isDead", dead);
            enemyCollider2d.enabled = false;
            audio.PlayOneShot(a);
            GameManager.instance.AddScore(5);
            t = Time.time;
            //animator.SetBool("isDead", dead);
            //t = Time.time;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(enemyRigidbody==null)
        {
            return;
        }

        if (!GameManager.instance.isGameover)
        {
            transform.Translate(Vector2.left * enemyspeed * Time.deltaTime);
        }

        // ���� ��ġ�� �������� �������� -width �̻� �̵������� ��ġ�� ����
        if (transform.position.x <= -20f)
        {
            dead = false;
            //animator.ResetTrigger("IsDead");
            animator.SetBool("isDead", dead);
            enemyCollider2d.enabled = true;
            
            Reposition();
            
            //Instantiate �� ���� ����: ���ο� ��ü ���� �� �ð� �ɸ�->���� �÷��� ���� ���� �߻� ����
        }

        if(dead==true)
        {
            if (Time.time > t + 2.0f)
            {
                Reposition();
                //dead = false;
                //animator.SetBool("isDead", dead);
            }
        }


    }

    private void Reposition()
    {
        //gameObject.SetActive(false);
        //gameObject.SetActive(true);
        Vector2 offset = new Vector2(-5, 25);
        transform.position = offset;
    }
}
