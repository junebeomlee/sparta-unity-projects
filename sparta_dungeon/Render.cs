namespace sparta_dungeon;

public class Renderer()
{
    private Dictionary<string, string> states = new Dictionary<string, string>();
    private Action<Renderer> content;
    private Action<Renderer, string> choices;
    
    public Renderer SetContent(Action<Renderer> content)
    {
        this.content = content;
        return this;
    }

    public Renderer SetChoices(Action<Renderer, string> choices)
    {
        this.choices = choices;
        return this;
    }

    // 제네릭 추가
    public (string, Action<string>) State<T>(string idexing, string? initValue = null)
    {
        Action<string> SetState = (string newValue) =>
        {
            states[idexing] = newValue;
        };
        
        if (states.TryGetValue(idexing, out string value))
        {
            return (value, SetState);
        }
        else
        {
            states.Add(idexing, initValue);
            return (initValue, SetState);
        }
    }

    public void Reset()
    {
        states.Clear();
    }

    public void Render()
    {
        Console.Clear();
        this.content(this);

        Console.WriteLine("");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.WriteLine(">>");
        
        string intput = Console.ReadLine();
        // 없는 경우 에러 발생
        this.choices(this, intput);
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
    
    public void Navigate(Pages.PageType pageType)
    {
        history.Push(pageType);
        if (Routes.ContainsKey(pageType))
        {
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
        INVENTORY_PAGE,
        STATUS_PAGE,
        SHOP_PAGE,
        LODGING_PAGE,
        DUNGEON_PAGE,
        DUNGEON_CLEAR_PAGE,
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
                    Console.WriteLine("");
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                    
                    Console.WriteLine("");
                    string[] choices = ["상태 보기", "인벤토리", "상점", "던전입장", "휴식하기"]; 
                    for (int i = 0; i < choices.Length; i++)
                    {
                        Console.WriteLine($"{i}. {choices[i]}");
                    }
                })
                .SetChoices((page, select) =>
                {
                    switch (select)
                    {
                        case "0": router.Navigate(PageType.STATUS_PAGE); break;
                        case "1": router.Navigate(PageType.INVENTORY_PAGE); break;
                        case "2": router.Navigate(PageType.SHOP_PAGE); break;
                        case "3": router.Navigate(PageType.DUNGEON_PAGE); break;
                        case "4": router.Navigate(PageType.LODGING_PAGE); break;
                    }
                })
            },
            {
                PageType.STATUS_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        var player = GameManager.Instance.Objects.Player;
                        var equipments = player.Equipped;
                        var attack = equipments.Where(equipment => equipment.Type == "attack").Sum(equipment => equipment.Value);
                        var defense = equipments.Where(equipment => equipment.Type == "defense").Sum(equipment => equipment.Value);
                        
                        Console.WriteLine("상태 보기");
                        Console.WriteLine("캐릭터의 정보가 표시됩니다.");

                        Console.WriteLine("");
                        Console.WriteLine($"LV. {player.Level}");
                        Console.WriteLine($"Chad ({player.Class})");
                        Console.WriteLine($"공격력 : {player.Attack} + [{attack}]");
                        Console.WriteLine($"방어력 : {player.Defense} + [{defense}]");
                        Console.WriteLine($"체력 : {player.CurrentHealth}");
                        Console.WriteLine($"Gold : {player.Gold} G");
                        
                        Console.WriteLine("");
                        string[] choices = ["나가기"];
                        for (int i = 0; i < choices.Length; i++)
                        {
                            Console.WriteLine($"{i}. {choices[i]}");
                        }
                    })
                    .SetChoices((page, select) =>
                    {
                        switch (select)
                        {
                            case "0": router.PopState(); break;
                        }
                    })
            },
            {
                PageType.INVENTORY_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        var player = GameManager.Instance.Objects.Player;
                        var mode = page.State<string>("mode", "normal");

                        Console.Write("인벤토리");
                        if(mode.Item1 == "equipped") Console.Write(" - 장착 관리");
                        Console.WriteLine("");

                        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                        Console.WriteLine("");

                        for (int i = 0; i < player.Inventory.Count; i++)
                        {
                            var item = player.Inventory[i];
                            var isEquipped = player.Equipped.Contains(item);
                            
                            // 조건 분리해서 정리하기
                            Console.WriteLine($"- {(mode.Item1 == "equipped" ? i + 1 : "")} {(isEquipped ? "[E]" : "")} {item.Name} | {(item.Type == "attack" ? "공격력" : "방어력")} +{item.Value} | {item.Description}");
                        }
                        Console.WriteLine("");
                        
                        string[] choices = [];
                        if (mode.Item1 == "normal") choices = ["장착 관리", "나가기"];
                        if (mode.Item1 == "equipped") choices = ["나가기"];
                        for (int i = 0; i < choices.Length; i++)
                        {
                            Console.WriteLine($"{i}. {choices[i]}");
                        }
                    })
                    .SetChoices((page, select) =>
                    {
                        var player = GameManager.Instance.Objects.Player;
                        var mode = page.State<string>("mode", "normal");

                        if (mode.Item1 == "normal")
                        {
                            switch (select)
                            {
                                case "0": mode.Item2("equipped"); break;
                                case "1": router.PopState(); break;
                            }
                        }
                        
                        if (mode.Item1 == "equipped")
                        {
                            if(select == "0") mode.Item2("normal");
                            else
                            {
                                Item selectedItem = player.Inventory[int.Parse(select) - 1];
                                
                                if (player.Equipped.Contains(selectedItem))
                                {
                                    player.Equipped.Remove(selectedItem);
                                }
                                else
                                {
                                    player.Equipped.Add(selectedItem);
                                }
                            }  
                        }
                       
                    })
            },
            {
                PageType.SHOP_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        var player = GameManager.Instance.Objects.Player;
                        var mode = page.State<Store.Mode>("mode", "normal");
                        var status = page.State<string>("status", "none");
                        
                        Console.WriteLine("상점");
                        Console.Write("필요한 아이템을 얻을 수 있는 상점입니다.");
                        if(mode.Item1 == "buying") Console.Write(" - 아이템 구매");
                        if(mode.Item1 == "selling") Console.Write(" - 아이템 판매");
                        Console.WriteLine("");
                        
                        Console.WriteLine("");
                        Console.WriteLine("[보유 골드]");
                        Console.WriteLine($"{player.Gold}G"); 
                        
                        Console.WriteLine("");
                        Console.WriteLine("[아이템 목록]");
                        for (int i = 0; i < Store.Items.Count; i++)
                        {
                            var item = Store.Items[i];
                            Console.WriteLine($"{(mode.Item1 == "normal" ? "-" : "- " + (i + 1))} {item.Name} | {(item.Type == "attack" ? "공격력" : "방어력")} +{item.Value} | {item.Description} | {item.Price} G");
                        }
                        
                        Console.WriteLine("");

                        string[] choices = [];
                        if (mode.Item1 == "buying") choices  = ["나가기"];
                        else choices = ["아이템 구매", "아이템 판매", "나가기"];
                        
                        for (int i = 0; i < choices.Length; i++)
                        {
                            Console.WriteLine($"{i}. {choices[i]}");
                        }

                        if (status.Item1 == "fail")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Gold 가 부족합니다.");
                            Console.ResetColor();

                        }

                        if (status.Item1 == "success")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("구매를 완료했습니다.");
                            Console.ResetColor();
                        }

                        if (status.Item1 == "exist")
                        {
                            Console.WriteLine("이미 구매한 아이템입니다.");

                        }
                        
                        Console.WriteLine("");

                    })
                    .SetChoices((page, select) =>
                    {
                        var player = GameManager.Instance.Objects.Player;
                        var store = GameManager.Instance.Objects.Store;
                        var mode = page.State<Store.Mode>("mode");
                        var status = page.State<string>("status");
                        
                        //  필터링 방식으로 처리
                        if (mode.Item1 == "buying")
                        {
                            if (select == "0")
                            {
                                status.Item2("normal");   
                                mode.Item2("normal");
                            }
                            else
                            {
                                // 로직 을 store에서 처리하도록 관리
                                if (player.Inventory.Contains(Store.Items[int.Parse(select) - 1]))
                                {
                                    status.Item2("exist");   
                                }
                                else {
                                    try
                                    {
                                        // 오류 날 수 있으므로, validate 필요
                                        store.Buy(Store.Items[int.Parse(select) - 1], player);
                                        status.Item2("success");   
                                    }
                                    catch (Exception e)
                                    {
                                        status.Item2("fail");   
                                    }
                                }
                            }
                        }
                        else
                        {
                            switch (select)
                            {
                                case "0": mode.Item2("buying"); break;
                                case "2": router.PopState(); break;
                            }
                        }
                    })
            },
            {
                PageType.LODGING_PAGE,
                new Renderer()
                    .SetContent((page) =>
                    {
                        var player = GameManager.Instance.Objects.Player;

                        Console.WriteLine("휴식하기");
                        Console.WriteLine($"500G를 내면 체력을 회복할 수 있습니다. (보유 골드: {player.Gold}G)");
                        
                        Console.WriteLine("");
                    })
                    .SetChoices((page, select) =>
                    {
                        
                    })
            },
            {
                PageType.DUNGEON_PAGE,
                new Renderer()
                    .SetContent(page =>
                    {
                        List<Dungeon> dungeons = GameManager.Instance.Objects.dungeons;

                        Console.WriteLine("던전입장");
                        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                        Console.WriteLine("");

                        for (var idx = 0;  idx < dungeons.Count; idx++)
                        {
                            Console.WriteLine($"{idx + 1}. {dungeons[idx].Name } | 방어력 {dungeons[idx].MinDependence} 이상 권장");
                        }
                        Console.WriteLine("0. 나가기");

                    })
                    .SetChoices((page, select) =>
                    {
                        List<Dungeon> dungeons = GameManager.Instance.Objects.dungeons;
                        
                    })
            },
            {
                PageType.DUNGEON_CLEAR_PAGE,
                new Renderer()
                    .SetContent(page =>
                    {
                        Console.WriteLine("던전 클리어");
                        Console.WriteLine("축하합니다!!\n쉬운 던전을 클리어 하였습니다.");
                        
                        Console.WriteLine("");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine("체력 100 -> 70");
                        Console.WriteLine("Gold 1000G -> 2200G");

                    })
                    .SetChoices((page, select) =>
                    {
                        switch (select)
                        {
                            case "0": router.PopState(); break;
                        }
                    })
            }

        };
    }
}