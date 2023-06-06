using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_AchiveManager : MonoBehaviour
{
    //���� ������ ����(����, �ҷ�����) -����Ż �켭����ũ14+����
    public GameObject[] lockMap;
    public GameObject[] unlockMap;

    public GameObject uiNotice;
    enum Achive2 {UnlockBoss}
    Achive2[] achive2;//���� �����͵��� �����ص� �迭 ���� �� �ʱ�ȭ
    WaitForSecondsRealtime wait;

    void Awake()
    {
        //Enum.GetValues �־��� �������� �����͸� ��� �������� �Լ�
        achive2 = (Achive2[])Enum.GetValues(typeof(Achive2));
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }

    }

    //���� ������ �ʱ�ȭ �Լ� �ۼ�
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        //���������� ������ ����
        foreach(Achive2 achive in achive2)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
        
    }

    void Start()
    {
        UnlockCharacter();
    }

    //ĳ���� ��ư �ر��� ���� �Լ�
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

    void CheckAchive(Achive2 achive2)//���� �޼��� ���� �Լ� ���Ӱ� �ۼ�
    {
        bool isAchive = false;

        switch(achive2)
        {
            case Achive2.UnlockBoss:
                isAchive = GameManager.instance.level >= 2;
                break;
        
            
        }

        if(isAchive && PlayerPrefs.GetInt(achive2.ToString()) == 0) //�ش� ������ ó�� �޼� �ߴٴ� ����, �ر� �ȵǾ�������
        {
            PlayerPrefs.SetInt(achive2.ToString(), 1);

            for(int index = 0; index< uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive2; //�˸� â�� �ڽ� ������Ʈ�� ��ȸ�ϸ鼭 ������ ������ Ȱ��ȭ
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }

        IEnumerator NoticeRoutine()//�˸� â�� Ȱ��ȭ�ߴٰ� ���� �ð� ���� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        {
            uiNotice.SetActive(true);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

            yield return wait;

            uiNotice.SetActive(false);
        }
    }
}
