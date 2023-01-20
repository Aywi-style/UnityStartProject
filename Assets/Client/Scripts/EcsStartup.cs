using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private GameState _gameState;
        private EcsSystems _firstSystems, _secondSystems;

        void Start()
        {
            _world = new EcsWorld();

            _gameState = GameState.Initialize(this);

            _firstSystems = new EcsSystems(_world, _gameState);
            _secondSystems = new EcsSystems(_world, _gameState);

            _firstSystems
                .Add(new FirstSystem())
                .Add(new SecondSystem())
                ;

            _secondSystems
                .Add(new ThirdSystem())
                .Add(new FourthSystem())
                ;
#if UNITY_EDITOR
            _firstSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                ;
#endif

            InjectAllSystems(_firstSystems, _secondSystems);
            InitAllSystems(_firstSystems, _secondSystems);
        }

        void Update()
        {
            _firstSystems?.Run();
            _secondSystems?.Run();
        }

        void OnDestroy()
        {
            OnDestroyAllSystems(_firstSystems, _secondSystems);

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }

        private void InjectAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Inject();
            }
        }

        private void InitAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Init();
            }
        }

        private void OnDestroyAllSystems(params EcsSystems[] systems)
        {
            for (int i = 0; i < systems.Length; i++)
            {
                if (systems[i] != null)
                {
                    systems[i].Destroy();
                    systems[i] = null;
                }
            }
        }
    }
}
