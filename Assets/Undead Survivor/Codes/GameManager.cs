using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0);  // 임시 스크립트 (첫번째 캐릭터 선택)
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }


    public void GameRetry()
    {
        SceneManager.LoadScene(0);    // LoadScene : 이름 혹은 인덱스로 장면을 새롭게 부르는 함수
    }


    void Update() {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp() {

        if (!isLive)
            return;

        exp++;
        
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])   //Min함수로 최고 경험치를 그대로 사용하도록 변경
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // 레벨업시 시간정지
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;     //timeScale : 유니티의 시간 속도(배율)

    }
    // 시간 작동
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

}
