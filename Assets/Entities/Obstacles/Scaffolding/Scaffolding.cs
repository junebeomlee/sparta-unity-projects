using UnityEngine;

public class Scaffolding : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;   
    private Vector3 _startPosition;

    void Update()
    {
        float offest = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = new Vector3(transform.position.x, transform.position.y, _startPosition.z + offest);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
