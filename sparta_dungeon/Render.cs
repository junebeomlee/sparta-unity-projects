namespace sparta_dungeon;

public class Renderer()
{
    private dynamic? _locationState = null;
    private Dictionary<string, string> _states = new Dictionary<string, string>();
    
    private Action<Renderer> _content = (page) => {};
    private Action<Renderer, int> _choices = (page, select) => {};
    
    public Renderer SetContent(Action<Renderer> content)
    {
        this._content = content;
        return this;
    }

    public Renderer SetChoices(Action<Renderer, int> choices)
    {
        this._choices = choices;
        return this;
    }
    
    public object? GetLocationState()
    {
        return _locationState;
    }
    
    public void SetLocationState(object? state)
    {
        this._locationState = state;
    }
    
    // 제네릭 추가
    public (string, Action<string>) State<T>(string idexing, string? initValue = null)
    {
        Action<string> setState = (string newValue) =>
        {
            _states[idexing] = newValue;
        };
        
        if (_states.TryGetValue(idexing, out string value))
        {
            return (value, setState);
        }
        else
        {
            _states.Add(idexing, initValue);
            return (initValue, setState);
        }
    }

    public void Reset()
    {
        SetLocationState(null);
        _states.Clear();
    }

    public async void Render()
    {
        Console.Clear();
        this._content(this);

        Console.WriteLine("");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.WriteLine(">>");
        
        string intput = Console.ReadLine();
        // 없는 경우 에러 발생
        if (int.TryParse(intput, out int select))
        { 
            this._choices(this, select);
        }
        this.Render();
    }
}


public class Router
{
    // 연결된 pages 객체로부터 타입 받기
    private Stack<Pages.PageType> history = new Stack<Pages.PageType>();
    public Dictionary<Pages.PageType, Renderer> Routes;
    
    public Router(Pages pages)
    {
        pages.SetRouter(this);
        pages.init();
    }
    
    public void Navigate(Pages.PageType pageType, dynamic? locationState = null)
    {
        history.Push(pageType);
        if (Routes.ContainsKey(pageType))
        {
            if(locationState != null) Routes[pageType].SetLocationState(locationState);
            Routes[pageType].Render();
        }
    }

    public void PopState()
    {
        if (history.Count <= 0)
        {
            throw new InvalidOperationException("no history found");
        }
        
        Pages.PageType currentPage = history.Peek();
        Routes[currentPage].Reset();
        history.Pop();
        
        Pages.PageType previousPage = history.Peek();
        if (Routes.ContainsKey(previousPage))
        {
            Routes[previousPage].Render();
        }; 
    }
}

public class Pages
{
    private Router router;

    public void SetRouter(Router router)
    {
        this.router = router;
    }
    
    public enum PageType
    {
        START_PAGE,
        SAVE_PAGE,
        INVENTORY_PAGE,
        STATUS_PAGE,
        SHOP_PAGE,
        LODGING_PAGE,
        DUNGEON_PAGE,
        DUNGEON_RESULT_PAGE,
        GAMEOVER_PAGE,
    }
    
    public void init()
    {
        router.Routes = new Dictionary<PageType, Renderer>()
        {
            {
                PageType.START_PAGE, 
                new Renderer()
                .SetContent((page) =>
                {
                    Player player = GameManager.Instance.ObjectsContext.Player;
                    
                    Console.WriteLine("");
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                    
                    Console.WriteLine("");
                    string[] choices = ["상태 보기", "인벤토리", "상점", "던전입장", "휴식하기", "저장하기"]; 
                    for (int i = 0; i < choices.Length; i++)
                    {
                        Console.WriteLine($"{i}. {choices[i]}");
                    }
                })
                .SetChoices((page, select) =>
                {
                    switch (select)
                    {
                        case 0: router.Navigate(PageType.STATUS_PAGE); break;
                        case 1: router.Navigate(PageType.INVENTORY_PAGE); break;
                        case 2: router.Navigate(PageType.SHOP_PAGE); break;
                        case 3: router.Navigate(PageType.DUNGEON_PAGE); break;
                        case 4: router.Navigate(PageType.LODGING_PAGE); break;
                        case 5: router.Navigate(PageType.SAVE_PAGE); break;
                    }
                })
            },
            {
                PageType.SAVE_PAGE,
                new Renderer()
                    .SetContent(page =>
                    {
                        var status = page.State<string>("status", "none");
                        
                        Console.WriteLine("저장하기");
                        Console.WriteLine("게임을 저장하시겠습니까?");
                        
                        Console.WriteLine("");
                        string[] choices = ["저장하기", "나가기"];
                        if (status.Item1 == "SAVED_LOADING") choices = [];
                        for(int i = 0; i < choices.Length; i++) Console.WriteLine($"{i}. {choices[i]}");

                        Console.WriteLine("");
                        switch (status.Item1)
                        {
                            case "SAVED_SUCCESS":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("저장이 완료되었습니다.");
                                Console.ResetColor();
                                break;
                            case "SAVED_FAIL":
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("저장을 실패했습니다.");
                                Console.ResetColor();
                                break;
                        }
                    })
                    .SetChoices((page, select) =>
                    {
                        var status = page.State<string>("status");

                        if (status.Item1 != "SAVED_LOADING")
                        {
                            switch (select)
                            {
                                case 0:
                                    GameManager.Instance.SaveGame();
                                    status.Item2("SAVED_SUCCESS");
                                    page.Render();
                                    break;
                                case 1: 
                                    router.PopState(); 
                                    break;
                            }
                        }
                    })
            },
            {
                PageType.STATUS_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        Player player = GameManager.Instance.ObjectsContext.Player;
                        int attackByEquipments = player.GetStatByEquipments("attack");
                        int defenceByEquipments = player.GetStatByEquipments("defense");
                        
                        Console.WriteLine("상태 보기");
                        Console.WriteLine("캐릭터의 정보가 표시됩니다.");

                        Console.WriteLine("");
                        Console.WriteLine($"LV. {player.Level}");
                        Console.WriteLine($"Chad ({player.Class})");
                        Console.WriteLine($"공격력 : {player.Attack} + [{attackByEquipments}]");
                        Console.WriteLine($"방어력 : {player.Defense} + [{defenceByEquipments}]");
                        Console.WriteLine($"체력 : {player.CurrentHealth}");
                        Console.WriteLine($"Gold : {player.Gold} G");
                        
                        Console.WriteLine("");
                        Console.WriteLine("0. 나가기");
                    })
                    .SetChoices((page, select) =>
                    {
                        if(select == 0) router.PopState();
                    })
            },
            {
                PageType.INVENTORY_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        Player player = GameManager.Instance.ObjectsContext.Player;
                        var mode = page.State<string>("mode", "NORMAL");
                        var status = page.State<string>("status", "NORMAL");

                        Console.Write("인벤토리");
                        if(mode.Item1 == "EQUIPMENT") Console.Write(" - 장착 관리");
                        Console.WriteLine("");

                        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                        Console.WriteLine("");

                        for (int i = 0; i < player.Inventory.Count; i++)
                        {
                            var item = player.Inventory[i];
                            var isEquipped = player.Equipped.Contains(item);
                            
                            // 조건 분리해서 정리하기
                            Console.WriteLine(
                                $"- {(mode.Item1 == "EQUIPMENT" ? i + 1 : "")} " +
                                $"{(isEquipped ? "[E]" : "")} {item.Name} | " +
                                $"{(item.Type == "attack" ? "공격력" : "방어력")} +{item.Value} |" +
                                $" {item.Description}"
                            );
                        }
                        Console.WriteLine("");
                        
                        string[] choices = [];
                        if (mode.Item1 == "NORMAL") choices = ["장착 관리", "나가기"];
                        if (mode.Item1 == "EQUIPMENT") choices = ["나가기"];
                        for (int i = 0; i < choices.Length; i++) Console.WriteLine($"{i}. {choices[i]}");

                        if (status.Item1 == "IS_ALREADY_EQUIPPED")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("관련 장비를 이미 착용 중입니다.");
                            status.Item2("NORMAL"); // 내역 제거
                        }
                    })
                    .SetChoices((page, select) =>
                    {
                        var player = GameManager.Instance.ObjectsContext.Player;
                        var mode = page.State<string>("mode");
                        var status = page.State<string>("status", "normal");

                        switch (mode.Item1)
                        {
                            case "NORMAL":
                                switch (select)
                                {
                                    case 0: mode.Item2("EQUIPMENT"); break;
                                    case 1: router.PopState(); break;
                                }
                                break;
                            case "EQUIPMENT":
                                switch (select)
                                {
                                    case 0: mode.Item2("NORMAL"); break;
                                    default:
                                        if (select - 1 < 0 || select > player.Inventory.Count)
                                        {
                                            break;
                                        }
                                        Item selectedItem = player.Inventory[select - 1];
                                        
                                        if (player.Equipped.Contains(selectedItem)) player.Equipped.Remove(selectedItem);
                                        else
                                        {
                                            if (player.Equipped.Any(equipment => equipment.Type == selectedItem.Type))
                                            {
                                                status.Item2("IS_ALREADY_EQUIPPED");
                                                break;
                                            }
                                            player.Equipped.Add(selectedItem);
                                        }
                                        break;
                                }
                                break;
                        }
                    })
            },
            {
                PageType.SHOP_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        var player = GameManager.Instance.ObjectsContext.Player;
                        var mode = page.State<Store.Mode>("mode", "NONE");
                        var status = page.State<string>("status", "NONE");
                        
                        Console.WriteLine("상점");
                        Console.Write("필요한 아이템을 얻을 수 있는 상점입니다.");
                        if(mode.Item1 == "BUY") Console.Write(" - 아이템 구매");
                        if(mode.Item1 == "SELL") Console.Write(" - 아이템 판매");
                        Console.WriteLine("");
                        
                        Console.WriteLine("");
                        Console.WriteLine("[보유 골드]");
                        Console.WriteLine($"{player.Gold}G"); 
                        
                        Console.WriteLine("");
                        Console.WriteLine("[아이템 목록]");

                        List<Item> items = mode.Item1 switch
                        {
                            "NONE" => Store.Items,
                            "BUY" => Store.Items,
                            "SELL" => player.Inventory,
                        };
                        
                        for (int i = 0; i < items.Count; i++)
                        {
                            var item = items[i];
                            Console.Write(
                                $"{(mode.Item1 == "NONE" ? "-" : "- " + (i + 1))} " +
                                $"{item.Name} | " +
                                $"{(item.Type == "attack" ? "공격력" : "방어력")} +{item.Value} | " +
                                $"{item.Description} | "
                            );
                            if(mode.Item1 == "BUY" || mode.Item1 == "NONE") Console.WriteLine($"{( player.Inventory.Contains(item) ? "구매완료" : item.Price + "G")}");
                            if(mode.Item1 == "SELL") Console.WriteLine($"{Math.Truncate(item.Price * 0.6f) + "G"}");
                        }
                        Console.WriteLine("");
                        
                        string[] choices = [];
                        if (mode.Item1 == "BUY" || mode.Item1 == "SELL") choices = ["나가기"];
                        else choices = ["아이템 구매", "아이템 판매", "나가기"];
                        for (int i = 0; i < choices.Length; i++) Console.WriteLine($"{i}. {choices[i]}");
                        
                        
                        if(mode.Item1 == "BUY") switch (status.Item1)
                        {
                            case "WRONG_INDEX":
                                Console.WriteLine("잘못된 번호입니다.");
                                break;
                            case "FAIL":
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Gold 가 부족합니다.");
                                Console.ResetColor();
                                break;
                            case "SUCCESS":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("구매를 완료했습니다.");
                                Console.ResetColor();
                                break;
                            case "EXIST":
                                Console.WriteLine("이미 구매한 아이템입니다.");
                                break;
                        }
                        
                        if(mode.Item1 == "SELL") switch (status.Item1)
                        {
                            case "WRONG_INDEX":
                                Console.WriteLine("잘못된 번호입니다.");
                                break;
                            case "SUCCESS":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("판매를 완료했습니다.");
                                Console.ResetColor();
                                break;
                        }
                    })
                    .SetChoices((page, select) =>
                    {
                        var player = GameManager.Instance.ObjectsContext.Player;
                        var store = GameManager.Instance.ObjectsContext.Store;
                        var mode = page.State<Store.Mode>("mode");
                        var status = page.State<string>("status");
                        
                            if(mode.Item1 == "BUY") switch (select)
                            {
                                case 0: 
                                    status.Item2("NONE");   
                                    mode.Item2("NONE");
                                    break;
                                default:
                                    if (select - 1 < 0 || select > Store.Items.Count)
                                    {
                                        status.Item2("WRONG_INDEX");
                                        break;
                                    }
                                    if (player.IsEquippedType(Store.Items[select - 1]))
                                    {
                                        status.Item2("EXIST");
                                        break;
                                    }
                                    if (!store.IsEnoughGold(item: Store.Items[select - 1], gold: player.Gold))
                                    {
                                        status.Item2("FAIL");
                                        break;
                                    }
                                    store.SendItemTo(Store.Items[select - 1], player);
                                    status.Item2("SUCCESS");   
                                    break;
                            }
                            
                            if(mode.Item1 == "SELL") switch (select)
                            {
                                case 0: 
                                    status.Item2("NONE");   
                                    mode.Item2("NONE");
                                    break;
                                default:
                                    if (select - 1 < 0 || select > player.Inventory.Count)
                                    {
                                        status.Item2("WRONG_INDEX");
                                        break;
                                    }
                                    
                                    store.GetItemFrom(player.Inventory[select - 1], player);
                                    status.Item2("SUCCESS");   
                                    break;
                            }                            
                            
                            if(mode.Item1 == "NONE") switch (select)
                            {
                                case 0: mode.Item2("BUY"); break;
                                case 1: mode.Item2("SELL"); break;
                                case 2: router.PopState(); break;
                            }
                    })
            },     {
                PageType.LODGING_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        var player = GameManager.Instance.ObjectsContext.Player;
                        var status = page.State<string>("status");

                        Console.WriteLine("휴식하기");
                        Console.WriteLine($"500G를 내면 체력을 회복할 수 있습니다. (보유 골드: {player.Gold}G)");
                        Console.WriteLine("");

                        Console.WriteLine("1. 휴식하기");
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine("");
                        
                        // switch로 변경
                        if (status.Item1 == Lodging.Status.MAX_HEALTH.ToString())
                        {
                            Console.WriteLine("현재 체력이 최대 체력입니다.");
                        }
                        if (status.Item1 == Lodging.Status.FAIL_REQUIRE_MONEY.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Gold 가 부족합니다.");
                            Console.ResetColor();
                        }

                        if (status.Item1 == Lodging.Status.SUCCESS.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("휴식을 완료했습니다.");
                            Console.ResetColor();
                        }
                        
                    })
                    .SetChoices((page, select) =>
                    {
                        Player player = GameManager.Instance.ObjectsContext.Player;
                        Lodging lodging = GameManager.Instance.ObjectsContext.Lodging;
                        
                        var status = page.State<string>("status");


                        switch (select)
                        {
                            case 1:
                                if (player.MaxHealth == player.CurrentHealth)
                                {
                                    status.Item2(Lodging.Status.MAX_HEALTH.ToString());
                                    break;
                                }
                                if (!lodging.IsAffordable(player.Gold))
                                {
                                    status.Item2(Lodging.Status.FAIL_REQUIRE_MONEY.ToString());
                                    break;
                                }
                                lodging.TakeRest(player);
                                status.Item2(Lodging.Status.SUCCESS.ToString());
                                break;
                            case 0: 
                                router.PopState(); 
                                break;
                        }
                    })
            },
            {
                PageType.DUNGEON_PAGE,
                new Renderer()
                    .SetContent(page =>
                    {
                        Player player = GameManager.Instance.ObjectsContext.Player;
                        List<Dungeon> dungeons = GameManager.Instance.ObjectsContext.Dungeons;

                        Console.WriteLine("던전입장");
                        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                        Console.WriteLine("");

                        for (var idx = 0;  idx < dungeons.Count; idx++) Console.WriteLine($"{idx + 1}. {dungeons[idx].Name } | 방어력 {dungeons[idx].MinDependence} 이상 권장");

                        Console.WriteLine("0. 나가기");
                    })
                    .SetChoices((page, select) =>
                    {
                        List<Dungeon> dungeons = GameManager.Instance.ObjectsContext.Dungeons;
                        Player player = GameManager.Instance.ObjectsContext.Player;

                        switch (select)
                        {
                            case 0: router.PopState(); break;
                            default:  // 전투가 여기서 발생하며 실질적 작업은 객체에서 정리된다.
                                Dungeon dungeon = GameManager.Instance.ObjectsContext.Dungeons[select - 1];
                                
                                // int prevGold = player.Gold;
                                // int prevHealth = player.CurrentHealth;
                                Player snapshot = player.Snapshot();
                                bool isVictory = dungeon.CheckVictory(player);
                                if (isVictory)
                                {
                                    dungeon.OnClear(player);
                                    player.LevelUp();
                                }
                                else dungeon.OnDefeat(player);
                                
                                if(player.CurrentHealth > 0)
                                router.Navigate(
                                    PageType.DUNGEON_RESULT_PAGE, 
                                    locationState: new {
                                        snapshot,
                                        isVictory,
                                        dungeon
                                    }
                                );
                                break;
                        }
                    })
            },
            {
                PageType.DUNGEON_RESULT_PAGE,
                new Renderer()
                    .SetContent(page =>
                    {
                        Player player = GameManager.Instance.ObjectsContext.Player;
                        // 자료형 확실히 넘길 수 있는 법에 대해 확인 필요
                        dynamic result = page.GetLocationState()!;
                        
                        // int prevHealth = (int)result.prevHealth;
                        // int prevGold = (int)result.prevGold;
                        Player snapshot = result.snapshot;
                        bool isVictory = result.isVictory;
                        Dungeon dungeon = (Dungeon) result.dungeon;
                        
                        if (isVictory)
                        {
                            Console.WriteLine("던전 클리어");
                            Console.WriteLine($"축하합니다!! {dungeon.Name}을 클리어 하였습니다.");
                        }
                        else
                        {
                            Console.WriteLine("던전 클리어 실패");
                            Console.WriteLine($"{dungeon.Name} 클리어를 실패하였습니다.");
                        }
                        
                        Console.WriteLine("");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"레벨: {snapshot.Level} -> {player.Level}");
                        Console.WriteLine($"체력: {snapshot.CurrentHealth} -> {player.CurrentHealth}");
                        Console.WriteLine($"Gold: {snapshot.Gold} -> {player.Gold}");
                        
                        Console.WriteLine("");
                        Console.WriteLine("0. 나가기");
                        
                    })
                    .SetChoices((page, select) =>
                    {
                        switch (select)
                        {
                            case 0: router.PopState(); break;
                        }
                    })
            },
            {
                PageType.GAMEOVER_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        Console.WriteLine("GAME OVER");
                        Console.WriteLine("플레이어가 사망했습니다.");
                        Console.WriteLine("");
                        Console.WriteLine("0. 다시하기");
                        
                    })
                    .SetChoices((page, select) =>
                    {
                        if(select == 0) GameManager.Instance.RestartGame();
                    })
            }

        };
    }
}