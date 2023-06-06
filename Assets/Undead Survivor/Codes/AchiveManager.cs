using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    //업적 데이터 관리(저장, 불러오기) -골드메탈 뱀서라이크14+참고
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    public GameObject uiNotice;
    enum Achive {UnlockPotato, UnlockBean}
    Achive[] achives;//업적 데이터들을 저장해둘 배열 선언 및 초기화
    WaitForSecondsRealtime wait;

    void Awake()
    {
        //Enum.GetValues 주어진 열거형의 데이터를 모두 가져오는 함수
        achives = (Achive[])Enum.GetValues(typeof(Achive));
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
        foreach(Achive achive in achives)
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
        for(int index = 0; index < lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)//업적 달성을 위한 함수 새롭게 작성
    {
        bool isAchive = false;

        switch(achive)
        {
            case Achive.UnlockPotato:
                if(GameManager.instance.isLive)
                    isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
            
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) //해당 업적이 처음 달성 했다는 조건, 해금 안되어있을때
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int index = 0; index< uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive; //알림 창의 자식 오브젝트를 순회하면서 수번이 맞으면 활성화
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
