using System.Collections.Generic;
using Contracts.Database;
using UnityEngine;

namespace Infrastructure.Database.Repositories
{
    [CreateAssetMenu(fileName = "ItemsRepositories", menuName = "ScriptableObjects/Configs/Items/ItemsRepositories", order = 1)]
    public class ItemsRepositories : ScriptableObject
    {
        [SerializeField] private List<ItemConfig> _itemConfigs;

        public List<ItemConfig> ItemConfigs => _itemConfigs;
    }
}