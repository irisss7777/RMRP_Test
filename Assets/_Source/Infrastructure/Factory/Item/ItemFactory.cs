using Contracts.Database;
using Contracts.Factory;
using Domain.Player.Inventory;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.View.Item;
using UnityEngine;
using Utils;
using Zenject;

namespace Infrastructure.Factory.Item
{
    public class ItemFactory : IItemFactory
    {
        [Inject] private readonly IItemsConfig _itemsConfig;
        [Inject] private readonly UniqueIdService _uniqueIdService;
        [Inject] private readonly MessageBus _messageBus;
        
        public void CreateItem(Vector3 position, string name, int id, bool isStackable, int maxStackCount, Color color, int itemCount = 1)
        {
            position = new Vector3(position.x, _itemsConfig.DefaultPosition.y, position.z);
            
            ItemModel model = new ItemModel(name, id, isStackable, maxStackCount, color, itemCount);

            ItemView view = GameObject.Instantiate(_itemsConfig.ViewPrefab as ItemView, position, Quaternion.identity);
            
            view.GetComponent<Renderer>().material.color = color;
            
            view.Initialize(model, _uniqueIdService.GenerateId<ItemView>(), _messageBus);
        }

        public Vector3 GetRandomPosition()
        {
            return new Vector3(
                _itemsConfig.DefaultPosition.x + Random.Range(-_itemsConfig.SpawnNoiseZone, _itemsConfig.SpawnNoiseZone),
                _itemsConfig.DefaultPosition.y,
                _itemsConfig.DefaultPosition.z + Random.Range(-_itemsConfig.SpawnNoiseZone, _itemsConfig.SpawnNoiseZone)
            );
        }
    }
}