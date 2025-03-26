using UnityEngine;

public abstract class PlayerComponent: MonoBehaviour
{
    private PlayerClass _player;
    
    protected PlayerComponent(PlayerClass playerClass)
    {
        _player = playerClass;
    }
}