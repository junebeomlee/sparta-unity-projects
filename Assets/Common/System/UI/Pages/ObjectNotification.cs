using TMPro;
using UnityEngine;

// refactor: 페이지가 무수히 많아질 수 있는 점에 대해서
public class ObjectNotifyPage : MonoBehaviour
{
    // ReSharper disable Unity.PerformanceAnalysis
    public void Set(ObjectNotification objectInformation)
    {
        transform.Find("Title").GetComponent<TMP_Text>().text = objectInformation.title;
        transform.Find("Description").GetComponent<TMP_Text>().text = objectInformation.description;
    }
}
