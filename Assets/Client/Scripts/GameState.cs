using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Client;

public class GameState
{
    private static GameState _gameState = null;

    #region Entities
    public const int NULL_ENTITY = -1;
    #endregion
    
    private GameState(in EcsStartup ecsStartup)
    {
        
    }

    public static GameState Initialize(in EcsStartup ecsStartup)
    {
        if (_gameState is null)
        {
            _gameState = new GameState(in ecsStartup);
        }

        return _gameState;
    }

    public static GameState Get()
    {
        return _gameState;
    }
}
