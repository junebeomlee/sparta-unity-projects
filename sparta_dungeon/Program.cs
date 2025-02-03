namespace sparta_dungeon;

class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        gameManager.LoadGame();
    }
}