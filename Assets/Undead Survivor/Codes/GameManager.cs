using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string targetSceneName = "BossMap1";
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int playerId;
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
    //swamp �߰� �ٸ� �ʿ��� dummy_swamp�� ����
    public Swamp swamp;
    public Swamp swamp1;
    public Swamp swamp2;
    public Swamp swamp3;
    public Lava lava0;
    public Lava lava1;
    public Lava lava2;
    public Lava lava3;
    public Ice ice1;
    public Ice ice2;
    public Ice ice3;
    public Ice ice4;


    void Awake()
    {
        instance = this;
    }


    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);  // �ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);//ȿ������ ����� �κи��� ����Լ� ȣ��
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

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
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

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }


    public void GameRetry()
    {
        SceneManager.LoadScene(0);    // LoadScene : �̸� Ȥ�� �ε����� ����� ���Ӱ� �θ��� �Լ�
    }


    void Update() {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene==targetSceneName)
        {
            if(gameTime <= 6f)
            {
                GetExp();
            }
        }
        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp() {

        if (!isLive)
            return;

        exp ++;
        
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])   //Min�Լ��� �ְ� ����ġ�� �״�� ����ϵ��� ����
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void TakeDamage(float damageAmount)//���� ������
    {
        if(!isLive) 
            return;

        health -= damageAmount;

        if(health <= 0)
        {
            health = 0;
            GameOver();
        }

    }


    // �������� �ð�����
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;     //timeScale : ����Ƽ�� �ð� �ӵ�(����)

    }
    // �ð� �۵�
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

}
