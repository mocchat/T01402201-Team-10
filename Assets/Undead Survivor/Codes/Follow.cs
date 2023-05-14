using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HP바 플레이어 하단에 위치 고정 
public class Follow : MonoBehaviour
{
    RectTransform rect;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        
        //WorldToScreenPoint : 월드 상의 오브젝트 위치를 스크린 좌표로 변환
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
