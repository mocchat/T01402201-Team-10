using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public float originalSpeed;
    public float currentSpeed;
    //lava
    public bool isOnLava;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);    //true 넣으면 비활성화한 애들도 사용가능
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
        /* swamp 속도 초기값 설정
        GameManager.instance.swamp.player_speed = speed;
        GameManager.instance.swamp1.player_speed = speed;
        GameManager.instance.swamp2.player_speed = speed;
        GameManager.instance.swamp3.player_speed = speed;
        GameManager.instance.swamp.true_speed = speed;
        GameManager.instance.swamp1.true_speed = speed;
        GameManager.instance.swamp2.true_speed = speed;
        GameManager.instance.swamp3.true_speed = speed;
        //lava 속도 초기값 설정
        GameManager.instance.lava0.player_speed = speed;
        GameManager.instance.lava1.player_speed = speed;
        GameManager.instance.lava2.player_speed = speed;
        GameManager.instance.lava3.player_speed = speed;
        GameManager.instance.lava0.true_speed = speed;
        GameManager.instance.lava1.true_speed = speed;
        GameManager.instance.lava2.true_speed = speed;
        GameManager.instance.lava3.true_speed = speed;
        */
    }
    private void Start()
    {
        originalSpeed = speed;
        currentSpeed = speed;
    }
    public void SetSpeed(float newSpeed)//swamp, lava 참조 함수
    {
        speed = newSpeed;
        currentSpeed = speed;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }



    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    // 02+ 강의에서 inputsystem을 이용한 플레이어 이동으로 바꾸어 구현.
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        anim.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        if(collision.transform.tag == "Enemy")
            GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0)
        {
            for ( int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");              //SetTrigger함수로 죽음 애니메이션 실행
            GameManager.instance.GameOver();

        }

    }


    public void ApplyLavaEffect()
    {
        //Lava에 영향을 받음
        float lavaSpeedReduction = GameManager.instance.LavaSpeedReduction;
        float newSpeed = originalSpeed - lavaSpeedReduction;
        SetSpeed(newSpeed);

        Debug.Log("hot");
        GameManager.instance.TakeDamage(0.5f);
    }
    public void RemoveLavaEffect()
    {
        SetSpeed(originalSpeed);
    }

}