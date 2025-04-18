using System.Collections.Generic;
using UnityEngine;


// + 금전
// 플레이어가 여려명이라 단일 인벤토리로 관리 필요
public class InventoryManager: MonoBehaviour
{
    public static readonly string Gold = "Gold";
    
    public int totalGold = 0;
    // 소모품은 ID로 가지고 실 사용 시에만 객체 생성을 통해 처리(메모리 관리)
    public List<string> ItemIds = new List<string>();
    public List<WearableItem> WearableItems = new List<WearableItem>();

    private void Awake()
    {
        GlobalManager.RegisterManager(this);
    }

    public void ModifyGold(int gold)
    {
        totalGold += gold;
        GlobalManager.GetManager<EventManager>().Notify(Gold, totalGold.ToString());        
    }
}