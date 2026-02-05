using Contracts.Database;
using Contracts.Presentation;
using Presentation.View.Item;
using UnityEngine;

namespace Infrastructure.Database.Configs
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "ScriptableObjects/Configs/Items/ItemsConfig", order = 1)]
    public class ItemsConfig : ScriptableObject, IItemsConfig
    {
        [SerializeField] private Vector3 _defaultPosition;
        [SerializeField] private ItemView _viewPrefab;
        [Range(0, 20)]
        [SerializeField] private float _spawnNoiseZone;

        public Vector3 DefaultPosition => _defaultPosition;
        public IItemView ViewPrefab => _viewPrefab;
        public float SpawnNoiseZone => _spawnNoiseZone;
    }
}