using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
   public Vector2 inputVec;
    public float speed;


    SpriteRenderer spr; 
    Rigidbody2D rigid;
    Animator anim;

     void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        
    }

    
    //물리 연산 프레임마다 호출되는 생명주기 함수 
      void FixedUpdate()
    {
        
        //normalized 벡터값의 크기 1이 되도록 좌표가 수정된
        //normalized를 사용 안하면 x축은 1 y축은 1로 움직이는데 대각선은 피타고라스정의로
        //루트2로 움직여서 더 빨리움직이는걸 방지하기위해 뱀서종류는 normalized사용 
        Vector2 nextVec = inputVec *speed * Time.fixedDeltaTime;
        //3. 위치 이동
        rigid.MovePosition(rigid.position + nextVec);
        
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void LateUpdate()
    {

      //magnitude 벡터의 순수한 크기 값
        anim.SetFloat("Speed",inputVec.magnitude);
       if (inputVec.x != 0)
        {
            spr.flipX = inputVec.x < 0;
            
        }
        
    }
}
