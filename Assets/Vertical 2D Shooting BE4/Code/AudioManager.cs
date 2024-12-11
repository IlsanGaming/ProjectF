using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;
    public enum Sfx {EnemyDead,EnemyHit,LevelUp=3,Lose,PlayerHit,Range=7,Select,Win,Pickup,Boom,Morse}

    void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer= bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers= new AudioSource[channels];

        for(int index=0;index<sfxPlayers.Length;index++)
        {
            sfxPlayers[index]=sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
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
    public void PlaySfx(Sfx sfx)
    {
        for(int index=0;index<sfxPlayers.Length;index++)
        {
            int loopIndex =(index+channelIndex)%sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            int ranIndex = 0;
            if(sfx==Sfx.EnemyHit||sfx==Sfx.PlayerHit)
            {
                ranIndex=Random.Range(0,2);
            }
            channelIndex=loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
    public void PlaySpecificBgm(AudioClip newClip)
    {
        bgmPlayer.Stop(); // 기존 BGM 정지
        bgmPlayer.clip = newClip; // 새로운 BGM 설정
        bgmPlayer.Play(); // 새로운 BGM 재생
    }
}
