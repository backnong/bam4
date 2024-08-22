using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

      void Awake()
    {//컴포넌트를 한번에많이가져옴 
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {

        timer += Time.deltaTime;
        //적절한 숫자로 나누어 시간에 맞춰 레벨이 올라가도록 작성 
        //Mathf.FloorToInt : 소숫점 아래는 버리고 Int형으로 바꾸는 함수
        //CeilToInt는 소숫점 아래를 올리고 Int 형으로 바꾸는 함수 
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
 

        if (timer >spawnData[level].spawnTime )
        {
             
            timer = 0;
            Spawn();
        }
 
    }


    void Spawn()
    {
        Debug.Log(level);
       GameObject enemy= GameManager.instance.pool.Get(0);
        //   spawnPoint = GetComponentsInChildren<Transform>();는 자기 자신도 포함이라 1부터해야 시작됌 0은아님 
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);

    }
}

//직렬화 코드위쪽에다가 넣으면 속성으로 들어감
// 직렬화의 장점은 직접 작성한 클래스를 직렬화를 통하여 인스펙터에서 초기화 가능 
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;

}
