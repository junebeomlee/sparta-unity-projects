public enum MiniGame
{
    Plane,
    Stack,
    Dungeon,
    Fishing,
    World
}

[System.Serializable]
public class GameScores
{
    public int planeMaxScore;
    public int stackMaxScore;
    public int dungeonMaxScore;
    public int fishingMaxScore;
}