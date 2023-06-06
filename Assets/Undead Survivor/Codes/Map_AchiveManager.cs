using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_AchiveManager : MonoBehaviour
{
    //업적 데이터 관리(저장, 불러오기) -골드메탈 뱀서라이크14+참고
    public GameObject[] lockMap;
    public GameObject[] unlockMap;

    public GameObject uiNotice;
    enum Achive2 {UnlockBoss}
    Achive2[] achive2;//업적 데이터들을 저장해둘 배열 선언 및 초기화
    WaitForSecondsRealtime wait;

    void Awake()
    {
        //Enum.GetValues 주어진 열거형의 데이터를 모두 가져오는 함수
        achive2 = (Achive2[])Enum.GetValues(typeof(Achive2));
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }

    }

    //저장 데이터 초기화 함수 작성
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        //순차적으로 데이터 저장
        foreach(Achive2 achive in achive2)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
        
    }

    void Start()
    {
        UnlockCharacter();
    }

    //캐릭터 버튼 해금을 위한 함수
    void UnlockCharacter()
    {
        for(int index = 0; index < lockMap.Length; index++)
        {
            string achiveName = achive2[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockMap[index].SetActive(!isUnlock);
            unlockMap[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achive2 achive2 in achive2)
        {
            CheckAchive(achive2);
        }
    }

    void CheckAchive(Achive2 achive2)//업적 달성을 위한 함수 새롭게 작성
    {
        bool isAchive = false;

        switch(achive2)
        {
            case Achive2.UnlockBoss:
                isAchive = GameManager.instance.level >= 2;
                break;
        
            
        }

        if(isAchive && PlayerPrefs.GetInt(achive2.ToString()) == 0) //해당 업적이 처음 달성 했다는 조건, 해금 안되어있을때
        {
            PlayerPrefs.SetInt(achive2.ToString(), 1);

            for(int index = 0; index< uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive2; //알림 창의 자식 오브젝트를 순회하면서 수번이 맞으면 활성화
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }

        IEnumerator NoticeRoutine()//알림 창을 활성화했다가 일정 시간 이후 비활성화하는 코루틴 생성
        {
            uiNotice.SetActive(true);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

            yield return wait;

            uiNotice.SetActive(false);
        }
    }
}
