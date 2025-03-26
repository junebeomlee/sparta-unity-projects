using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/Item")]
public class ItemSO : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public Sprite sprite;


}

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Scriptable Objects/Item/ConsumableItem")]
public class ConsumableItem : ItemSO
{
    public float amount; //
    public int repeatTime; // 일정 시간을 기준으로 반복하는 형식
}

[CreateAssetMenu(fileName = "BuffItem", menuName = "Scriptable Objects/Item/BuffItem")]
public class BuffItem : ItemSO
{
    public int duration; // 지속 시간
}