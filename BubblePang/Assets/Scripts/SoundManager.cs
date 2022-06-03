using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Space]
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioClip[] bgmClips;
    [Space]
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioClip[] sfxClips;

    public enum BGM { PLAY };
    public enum SFX { START, PANG };

    private static SoundManager instance = null;
    public static SoundManager Instance { 
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayBGM(BGM index)
    {
        bgm.clip = bgmClips[(int)index];
        bgm.Play();
    }

    public void PlaySFX(SFX index)
    {
        sfx.PlayOneShot(sfxClips[(int)index]);
    }

    public void OutBGM()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (bgm.volume > 0)
        {
            bgm.volume -= 0.02f;
            yield return new WaitForSeconds(0.1f);
        }
        bgm.Stop();
    }
}
