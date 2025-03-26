using System.Collections.Generic;
using UnityEngine;

public class BattleSystem: MonoBehaviour
{
    // 진행 순서를 위한
    private Queue<Controller> _players = new();
}