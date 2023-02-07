using UnityEngine;
using Client;

public class GameState
{
    private static GameState _gameState = null;

    public AllPools AllPools;
    public AllPools ActivePools;

    #region Entities
    public const int NULL_ENTITY = -1;
    #endregion

    #region Audio
    [field:SerializeField]
    public AudioMixer AudioPack { private set; get; }
    [field: SerializeField]
    public AmbientType AmbientType { private set; get; }

    public bool IsNeedChangeEmbient = false;
    #endregion
    
    private GameState(in EcsStartup ecsStartup)
    {
        AllPools = ecsStartup.AllPools;

        AudioPack = ecsStartup.AudioPack;
        AmbientType = ecsStartup.AmbientType;
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
