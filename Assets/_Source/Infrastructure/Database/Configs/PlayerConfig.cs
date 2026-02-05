using Contracts.Database;
using Contracts.Presentation;
using Presentation.View.Player;
using UnityEngine;

namespace Infrastructure.Database.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Configs/Player/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject, IPlayerConfig
    {
        [SerializeField] private float _speed;
        [SerializeField] private PlayerView _playerView;

        [Range(12, 24)] [SerializeField] private int _inventorySlotCount;

        public float Speed => _speed;
        public IPlayerView PlayerViewPrefab => _playerView;
        public int InventorySlotCount => _inventorySlotCount;
    }
}