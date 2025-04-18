using System.Collections;
using UnityEngine;

public class CameraManager: MonoBehaviour
{

    // 카메라 거치대 개념을 넣어야 할까.
    private Transform cameraRig; //자기 자신
    private Camera currentCamera;
    
    public Transform target; // 타겟이 매번 바뀜, 매니저로 연결 필요
    
    public View currentView;

    private void Awake()
    {
        
        GlobalManager.RegisterManager(this);
        
        cameraRig = GetComponent<Transform>();
        currentCamera = GetComponentInChildren<Camera>();
        
        Controls controls = new Controls();
        controls.Enable();
        // controls.Player.Click.performed +=
    }

    public void ChangeView(View view)
    {
        currentView = view;
    }

    public void SetTarget(Transform target)
    {
        
    }
    
    private void Update()
    {
        if (!this.target) return;
        transform.position = Vector3.Slerp(transform.position, new(target.position.x, 10, target.position.z), 2f);
    }

    public void Shake()
    {
        transform.position = new(target.position.x, 10, target.position.z);
        
        float shakeDuration = 0.2f;
        float shakeMagnitude = 0.5f;

        var currTarget = target;
        var currPosition = transform.position;

        target = null;
        
        IEnumerator ShakeCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                float x = Random.Range(-1f, 1f) * shakeMagnitude;
                float y = Random.Range(-1f, 1f) * shakeMagnitude;

                // Apply the shake to the camera
                transform.position = currPosition + new Vector3(x, y, 0f);

                elapsedTime += Time.deltaTime;

                // Smooth the shake effect by decreasing shake duration over time
                yield return null;
            }

            target = currTarget;
        }
        
        StartCoroutine(ShakeCoroutine());
    }
}