namespace sparta_dungeon;

public class Objects
{
    public Player Player = new Player();
    public Store Store = new Store();
    public List<Dungeon> dungeons = new List<Dungeon>()
    {
        new Dungeon(name: "쉬운 던전", minDependence: 5, gold: 1000),
        new Dungeon(name: "일반 던전", minDependence: 11, gold: 1700),
        new Dungeon(name: "어려운 던전", minDependence: 17, gold: 2500),
    };
}