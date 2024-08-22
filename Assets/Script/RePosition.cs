using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour

     
 {

    Collider2D coll;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        
         if (!collision.CompareTag("Area"))
      
           return;
             
        
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 mypos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - mypos.x);
        float diffY = Mathf.Abs(playerPos.y - mypos.y);

        //플레이어의 이동 방향을 저장하기 위한 변수 
        Vector3 PlayerDir = GameManager.instance.player.inputVec;
        float dirX = PlayerDir.x < 0 ? -1 : 1;
        float dirY = PlayerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }


                break;
            case "Enemy":
                //콜라이더가 활성화 되어있는지 조건 먼저 작성 
                if (coll.enabled)
                {
                    transform.Translate(PlayerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;


        }
    }
}
