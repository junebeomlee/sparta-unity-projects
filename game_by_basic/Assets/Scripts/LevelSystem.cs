public class LevelSystem
{
    private Stats _stats;
    private ResourceController _resourceController;

    public LevelSystem(Stats stats, ResourceController resourceController)
    {
        // 두 정보 모두 영향을 주므로 객체를 가져온다.
        _stats = stats;
        _resourceController = resourceController;
    }

    public void LevelUp()
    {
        _stats.Level++;
        
        // 스탯의 고정 데이터 자체에 변화를 준다.(캡슐화가 필요한 부분인지?)
        _stats.Health += 50;
        _stats.Power += 10;
        
        
    }
}