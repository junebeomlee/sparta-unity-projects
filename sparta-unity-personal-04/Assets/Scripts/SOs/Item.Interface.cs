public interface ItemInterface
{

    public void UseItem()
    {
        
    }
}

public class ConsumableItem : ItemInterface
{
    public ItemSO itemSO;
    
    public void UseItem()
    {
        // itemSO.amount;
    }
}

public class WearableItem : ItemInterface
{
    // equip으로 발생(어댑터 패턴)
    public void UseItem() {}
}