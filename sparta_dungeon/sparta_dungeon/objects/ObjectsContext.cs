
using sparta_dungeon;

public class ObjectsContext()
{
    public Player Player = new Player();
    public readonly Store Store = new Store();
    public readonly Lodging Lodging = new Lodging(cost: 500);
    public readonly List<Dungeon> Dungeons =
    [
        new Dungeon(name: "쉬운 던전", minDependence: 5, gold: 1000),
        new Dungeon(name: "일반 던전", minDependence: 11, gold: 1700),
        new Dungeon(name: "어려운 던전", minDependence: 17, gold: 2500)
    ];
}