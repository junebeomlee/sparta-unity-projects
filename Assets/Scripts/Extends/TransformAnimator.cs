using System.Collections;
using UnityEngine;

public static class TransformAnimator
{
    public static void Transition(this Transform transform, Vector3 destination, float duration)
    {
        transform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(MoveCoroutine(transform, destination, duration));
    }
    
    private static IEnumerator MoveCoroutine(Transform transform, Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            yield return null;
        }

        transform.position = target;
    }
}
