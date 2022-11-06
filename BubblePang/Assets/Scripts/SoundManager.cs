using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Space]
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;
    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void PlayBGM()
    {
        bgm.Play();
    }
    
    public void FadeOutBGM()
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
