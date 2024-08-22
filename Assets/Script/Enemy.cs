using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    //  하나가 아니다보니 배열로 생성 
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

     void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


      void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }
        //타켓 = 플레이어 , 리지드 포지션 좀비포지션 
        Vector2 dirVec = target.position - rigid.position;

        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
     
        rigid.MovePosition(rigid.position + nextVec);
        //velocity가 제로가아니면 플레이어가 밀려나서 이런 로직 작성 
        rigid.velocity = Vector2.zero;
    }

      void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        //좀비가 플레이어보다 앞쪽 ex x:3 플레이어 x:1 이면 돌아봐야하니 이런 컨디션으로
        spriter.flipX = target.position.x < rigid.position.x;
    }

    //스크립트가 활성화 될 때 , 호출되는 이벤트 함수 
      void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

    }

    public void Init(SpawnData data)
    {//어떤걸로 바꿀래 이걸로 바꾸겠습니다 ?
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

      void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        if (health > 0)
        {
            //.. Live , Hit Ation
        }
        else
        {
            //.. Die
            Dead();
        }
        
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }


}
