using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour 
{
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트
   public AudioClip a; // 사용할 오디오 소스 컴포넌트


    private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update() {
       // 사용자 입력을 감지하고 점프하는 처리
       if (isDead)
       {
            return;
       }

       if(transform.position.x<-3.5f)
        {
            /*if(isGrounded==true)
            {
                playerRigidbody.AddForce(new Vector2(3f, 0));
                if (transform.position.x > -3.0f)
                {
                    return;
                }
            }*/
            if (isGrounded == false)
            {
                playerRigidbody.AddForce(new Vector2(3f, 0));
                if (transform.position.x > -3.1f)
                {
                    return;
                }
            }

        }

        if (Input.GetMouseButtonDown(0)&&jumpCount<2)
        {
            jumpCount++;

            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            playerAudio.PlayOneShot(a); // Play(): audiosource 소리 1개만 나옴(소리 여러 개 나오는 상황인 경우 1개만 송출, 나머지는 소리 죽음) -> Playoneshot(): 소리 여러 개 동시에 나옴
        }
       else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y>0) // y 양수: 점프 동작해 올라가는 중, y 음수: 점프 최고점 달성 후 아래로 떨어짐
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; // 점프 높이 조절 가능(오래 누르면 높이 점프, 짧게 누르면 짧게 점프)

        }

        animator.SetBool("Grounded", isGrounded); // 파라미터 Grounded에 isGrounded 값 입력(땅에 닿으면 isGrounded == false -> Grounded == false, 떠 있으면 Ture)->해당하는 애니메이션 재생) 


       
   }

   private void Die() {
        // 사망 처리
        animator.SetTrigger("Die"); // Die 파라미터의 트리거 발동

        //playerAudio.clip = deathClip;
        playerAudio.PlayOneShot(deathClip);
        playerRigidbody.velocity = Vector2.zero;
        
        isDead = true;

        GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other) { // 특정 조건(==isTrigger 있는 물체와 부딪힐 때) 때 발동
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       if(other.tag=="Dead"&&!isDead)
       {
            Die();
       }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y>0.3f) // contacts: 부딛힌 위치를 배열로 받음->contacts[0]: 첫 번째로 부딛힌 위치, normal==normal vector(면에 수직인 벡터), 부딛힌 면의 경사가 45도 미만이면 그냥 평면 땅으로 인식(45도 넘으면 절벽으로 인식)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}