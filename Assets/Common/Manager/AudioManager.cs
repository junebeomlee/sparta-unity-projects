using System.Collections.Generic;
using UnityEngine;

// 다른 방식: 폴더에서 파일 검색(사용하는 폴더에서만)
[RequireComponent(typeof(AudioSource))]
public class AudioManager : Manager
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public List<AudioClip> bgmClips;
    public List<AudioClip> sounds;

    public void PlayBGM(string name)
    {
        AudioClip clip = bgmClips.Find(sound => sound.name == name);
        if (!clip) Debug.LogWarning("PlayBGM: Sound not found: " + name);
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    
    // 오디오가 재생되는 위치가 다를 수 있는 점에 대해서
    public void PlaySFX(string name)
    {
        AudioClip clip = sounds.Find(sound => sound.name == name);
        if(!clip) Debug.LogWarning("Sound: " + name + " not found!");
        sfxSource.PlayOneShot(clip);
    }
}
