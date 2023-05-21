using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;//어떤 bgm인지
    public float bgmVolum;//volum
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    public AudioClip[] sfxClips;//어떤 bgm인지
    public float sfxVolum;//volum
    public int channels;//다량의 효과음을 낼 수 있도록 채널 개수 변수 선언
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx {Dead,Hit,LevelUp=3,Lose,Melee,Range=7,Select,Win}

    void Awake()
    {
        instance = this;
        Init();
    }
    
    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform; //배경음을 담당하는 자식 오브젝트 생성
        bgmPlayer = bgmObject.AddComponent<AudioSource>();//AddComponent 함수로 오디오소스를 생성하고 변수에 저장
        bgmPlayer.playOnAwake = false;//기본값 true
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolum;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform; //배경음을 담당하는 자식 오브젝트 생성
        sfxPlayers = new AudioSource[channels];

        for(int index = 0;index<sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolum;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else 
        {
            bgmPlayer.Stop();
        }
    }
    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }


    //효과음 재생 함수
    public void PlaySfx(Sfx sfx)
    {
        for(int index=0; index<sfxPlayers.Length;index++) 
        {
            //채널 개수만큼 순회하도록 채널인덱스 변수 활용
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;//랜덤 재생
            if(sfx == Sfx.Hit || sfx ==Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);//효과음이 2개 이상인 것은 랜덤 인덱스를 더하기
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
