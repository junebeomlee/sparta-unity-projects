using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManager: MonoBehaviour
{
    private AudioSource _backgroundAudio;
    private AudioSource _soundAudio;
    private List<AudioSource> _individualAudios = new();
    
    public enum SoundType { BGM, SFX }

    void Awake()
    {
        GlobalManager.RegisterManager(this);
    }
    

    public void RegisterIndividualAudio(AudioSource source)
    {
        _individualAudios.Add(source);
    }

    public void ChangeVolume(SoundType soundType, float volume)
    {
        if (soundType == SoundType.BGM)
        {
            _backgroundAudio.volume = volume;
        }

        if (soundType == SoundType.SFX)
        {
            _soundAudio.volume = volume;
            _individualAudios.ForEach(audio => audio.volume = volume);
        }
    }

    // 그냥 resource에서 가지고 있는 편이 나을 듯
    public void GenerateSoundFrom(Transform currTransform, string address)
    {
        AudioClip audioClip;
        
        AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(address);
        handle.Completed += (operation) =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                audioClip = operation.Result;
                // Debug.Log($"Loaded audio clip: {audioClip}");
                
                var instance = new GameObject("sound");
                instance.transform.SetParent(currTransform);
                instance.transform.position = Vector3.zero;

                var audioSource = instance.AddComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.PlayOneShot(audioClip);
        
                IEnumerator DestroyAfterSound(GameObject instance, float duration) { yield return new WaitForSeconds(duration); Destroy(instance); }
                StartCoroutine(DestroyAfterSound(instance, audioClip.length));
            }
            else
            {
                Debug.LogError($"Failed to load audio clip: ");
            }
        };
    }
}