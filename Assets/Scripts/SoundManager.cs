using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgmAudioSource; //BGM用のAudioSource
    private List<AudioSource> seAudioSourceList = new List<AudioSource>(); //サウンドエフェクト用のAudioSourceのリスト
    [SerializeField]
    private List<AudioClip> bgmClipList = new List<AudioClip>(); //BGMのクリップのリスト
    [SerializeField]
    private List<AudioClip> seClipList = new List<AudioClip>(); //サウンドエフェクトのクリップのリスト
    private float volume = 0.5f; //音量

    //スライダーの値が変化したときに音量を変化させる関数。
    public void ChangeVolume(System.Single val)
    {
        AudioSource[] j = gameObject.GetComponents<AudioSource>();
        foreach(AudioSource a in j) 
        {
            a.volume = val;
            volume = val;
        }
    }
    void Awake()
    {
        //BGM用のAudioSourceを設定し、プレイする
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = 0.5f;
        PlayBGM(0);

        //各クリップに1つのオーディオソースを追加する
        for (int i = 0; i < seClipList.Count; i++)
        {
            seAudioSourceList.Add(gameObject.AddComponent<AudioSource>());
        }

        DontDestroyOnLoad(gameObject);
    }

    //使用されていないSE用のAudioSourceを取得する
    //現在使用されていないAudioSourceがない場合は新しいのを作成する
    private AudioSource GetUnusedAudioSource()
    {
        for (int i = 0; i < seAudioSourceList.Count; i++)
        {
            if (!seAudioSourceList[i].isPlaying)
            {
                return seAudioSourceList[i];
            }
        }

        AudioSource audio = gameObject.AddComponent<AudioSource>();
        seAudioSourceList.Add(audio);
        return audio;
    }

    public void PlayBGM(int i)
    {
        var clip = bgmClipList[i];
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void PlaySE(int i)
    {
        var audioSource = GetUnusedAudioSource();
        audioSource.volume = volume;
        var clip = seClipList[i];
        if (audioSource == null)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(DestroyAudioSourceWhenFinished(audioSource));
    }

    //終了したらAudioSourceを削除するコルーチン
    private IEnumerator DestroyAudioSourceWhenFinished(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);   
        seAudioSourceList.Remove(audioSource);
        Destroy(audioSource);
    }
}
