namespace sparta_dungeon;

using System.Text.Json;

public class GameManager
{
    public static GameManager Instance { get; } = new GameManager();
    private const string SavedFilePath = "savedGame.json";

    public ObjectsContext ObjectsContext = new ObjectsContext();
    private Router? _router;

    public void StartGame()
    {
        _router = new Router(new Pages());
        _router.Navigate(Pages.PageType.START_PAGE);
    }

    public void RestartGame()
    {
        Instance.ObjectsContext.Player = new Player();
        StartGame();
    }
    
    public void LoadGame()
    {
        if (!File.Exists(SavedFilePath))
        {
            StartGame();
        }
        else {
            Console.WriteLine("[스파르타 던전]");
            Console.WriteLine("저장된 게임이 있습니다. 불러오시겠습니까? (y/n)");
            string? input = Console.ReadLine();
            
            if (input.ToLower() == "y")
            {
                string json = File.ReadAllText(SavedFilePath);
                Player player = JsonSerializer.Deserialize<Player>(json);
                Instance.ObjectsContext.Player = player;
                StartGame();

            }
            else
            {
                StartGame();
            }
        }
    }
    public void SaveGame()
    {
        Player player = this.ObjectsContext.Player;
        string json = JsonSerializer.Serialize(player);
        File.WriteAllTextAsync(SavedFilePath, json);
    }

    public void GameOver()
    {
        Instance._router.Navigate(Pages.PageType.GAMEOVER_PAGE);
    }
}