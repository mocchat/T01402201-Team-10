using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //5가지 정보에 대한 열거형
    public enum InfoType { Exp, Level, Kill, Time, Health}

    public InfoType type;

    Text myText;
    Slider mySlider;


    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level);
                break;
            case InfoType.Time:
                //흐르는 시간이 아닌 남은 시간부터 구하기
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                // 분 = 60으로 나누어 분을 구함, 초 = 60으로 나눈 나머지, Mathf로 소수점 버리기
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);  // D0,D1,D2 ... : 자리수 지정
                break;
        }
    }








}
