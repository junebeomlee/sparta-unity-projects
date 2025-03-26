using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roullete : MonoBehaviour
{
    [SerializeField]
    private float Speed = 300.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MainCamera MainCamera;
    
    private SpriteRenderer spriteRenderer;

    private IEnumerator Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        while(true)
        {
            int time =  Random.Range(1, 5);
            yield return new WaitForSeconds(time);
            Debug.Log("코루틴 테스트");
        }
    }

    public void Hit()
    {
        StopCoroutine("OnHit");
        StartCoroutine("OnHit");
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = new Color(0.827f, 0.827f, 0.827f, 1f);;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * Speed);    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(transform);
        MainCamera.TriggerShake(0.05f, 0.2f);

    }
}
