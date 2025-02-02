namespace sparta_dungeon;

public class Objects
{
    public Player Player = new Player();
    public Store Store = new Store();
    public List<Dungeon> dungeons = new List<Dungeon>()
    {
        new Dungeon(name: "beginner", minDependence: 5, gold: 1000),
        new Dungeon(name: "normal", minDependence: 11, gold: 1700),
        new Dungeon(name: "hard", minDependence: 17, gold: 2500),
    };
}