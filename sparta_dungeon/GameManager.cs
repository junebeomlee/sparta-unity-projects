namespace sparta_dungeon;

using System.Text.Json;

// 게임 정보 저장할 때 Serialize 같은 거임
public class GameState
{
    public int Level { get; set; }
    public int Health { get; set; }
    public int Score { get; set; }
}


public class GameManager
{
    public static GameManager Instance { get; } = new GameManager();
    private string savedFilePath = "savedGame.json";

    public Router Router = new Router(new Pages());
    public Objects Objects = new Objects();

    public void StartGame()
    {
        Router.Navigate(Pages.PageType.START_PAGE);
    }
    
    public void LoadGame()
    {
        if (!File.Exists(savedFilePath))
        {
            Console.WriteLine("저장된 게임이 없습니다. 새로운 게임을 불러옵니다.");
            StartGame();
        }
        else {
            Console.WriteLine("저장된 게임이 있습니다. 불러오시겠습니까?(y/n)");
            string input = Console.ReadLine();
            
            if (input.ToLower() == "y")
            {
                string json = File.ReadAllText(savedFilePath);
                GameState gameState = JsonSerializer.Deserialize<GameState>(json);
            }
            else
            {
                Console.WriteLine("새로운 게임을 시작합니다.");
                StartGame();
            }
        }
    }
    public async Task<bool> SaveGame(GameState gameState)
    {
        try
        {
            var json = JsonSerializer.Serialize(gameState);
            await File.WriteAllTextAsync(savedFilePath, json);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}