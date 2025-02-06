using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Pin : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("Speed")]
    private float speed = 300.0f;
    
    [HideInInspector]
    public bool IsThrow = false;
    [HideInInspector]
    public bool IsPinned = false;

    public AudioClip CrachSound;
    private AudioSource _audioSource;
    
    public Transform CrashRespawnPosition;
    public GameObject CrashEffect;
    
    void Awake()
    {
        this.Reset();
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Reset()
    {
        this.IsThrow = false;
        this.IsPinned = false;
    }

    public void Throw()
    {
        IsThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
    

        if (!IsPinned && IsThrow)
        {
            transform.Translate(Vector2.up * (Time.deltaTime * speed));
        }
        
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
            // transform.Translate(Vector2.up * (Time.deltaTime * 3f));
        // }
        
        // if (Input.GetKey(KeyCode.Space))  // 스페이스바를 누르고 있을 때
        // {
           // transform.Translate(Vector2.up * (Time.deltaTime * speed));
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag($"Roulette"))
        {
            IsPinned = true;
            if (CrachSound != null)
            {
                _audioSource.clip = CrachSound;
                _audioSource.Play();
            }
            collision.gameObject.GetComponent<Roullete>().Hit();
            Instantiate(CrashEffect, CrashRespawnPosition.position, CrashRespawnPosition.rotation); 
            
            Manager.Instance.CreatePin();
        };
    }
}
