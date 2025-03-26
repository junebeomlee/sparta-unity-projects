using System.Collections;
using TMPro;
using UnityEngine;

public class BattleInfoPage: MonoBehaviour
{
    private EventManager _eventManager;
    public TMP_Text goldText;
    
    public void Start()
    {
        _eventManager = GlobalManager.GetManager<EventManager>();
        _eventManager.Subscribe(InventoryManager.Gold, (value) => { goldText.text = value; });
    }

    IEnumerator IncreaseTransition(int prevValue, int newValue)
    {
        while (prevValue != newValue)
        {
            prevValue += 1;
            yield return new WaitForSeconds(0.5f);
        }
    }
}