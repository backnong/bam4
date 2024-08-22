using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //..프리팹들을 보관할 변수
    public GameObject[] prefabs;
    //..풀 담당을 하는 리스트들
    List<GameObject>[] pools;

      void Awake()
    {
        //리스트 배열 변수 초기화 할때는 크기는 프리펩 배열 길이 활용 
        pools = new List<GameObject>[prefabs.Length];

        for(int i=0; i<pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        Debug.Log(pools[0]);

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ...선택한 풀의 놀고 있는 게임 오브젝트 접근 (비활성화)

             

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //..발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
            }
        }

        //.. 못 찾았으면 ?
        //null이면 false임
        if (!select)
        {
            //... 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

       

        return select;
    }


}
