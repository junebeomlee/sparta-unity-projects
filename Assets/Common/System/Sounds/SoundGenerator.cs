using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundGenerator : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player;

    public float maxDistance = 10f;
    public float minDistance = 2f;
    public float maxVolume = 1f;
    public float minVolume = 0.1f;
    
    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // distance가 지나칙 멀면 소리가 날 필요가 없는 점에 대하여
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float t = (distance - minDistance) / (maxDistance - minDistance);
        audioSource.volume = Mathf.Lerp(maxVolume, minVolume, t);
    }
}
