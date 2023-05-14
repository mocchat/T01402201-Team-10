using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }
   
    void Next()
    {
       
        // 1. 모든 아이템 비활성화
        foreach(Item item in items)               //foreach를 활용하여 모든 아이템 오브젝트 비활성화
        {
            item.gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];                        // 랜덤으로 활성화 할 아이템의 인덱스 3개를 담을 배열 선언
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])    // 서로 비교하여 모두 같지 않으면 반복문 빠져나감
                break;
        }
        

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];


            // 3. 만렙 아이템의 경우는 소비아이템으로 대체
            if (ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            { 
                ranItem.gameObject.SetActive(true);
            }
            
        }

    }

}
