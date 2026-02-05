using Application.Player;
using Contracts.Database;
using Contracts.Factory;
using Contracts.Presentation;
using Domain.Player;
using Domain.Player.Inventory;
using Infrastructure.Factory.Inventory;
using Infrastructure.Factory.Item;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.Presenters.Player;
using Presentation.View.Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Infrastructure.Factory.Player
{
    public class PlayerFactory
    {
        [Inject] private readonly InventoryFactory _inventoryFactory;
        [Inject] private readonly ItemFactory _itemFactory;
        [Inject] private readonly MessageBus _messageBus;
        [Inject] private readonly UniqueIdService _uniqueIdService;
        [Inject] private ICameraView _camera;

        public void CreatePlayer(IPlayerConfig config)
        {
            var inventoryPresenter = _inventoryFactory.CreateInventory(config);
            var model = CreateModel(config);
            var presenter = CreateView(config);
            CreateServices(model, presenter, inventoryPresenter);
        }

        private PlayerModel CreateModel(IPlayerConfig config)
        {
            return new PlayerModel(config.Speed, CreateInventory(config), _uniqueIdService.GenerateId<PlayerModel>());
        }

        private InventoryModel CreateInventory(IPlayerConfig config)
        {
            return new InventoryModel(config.InventorySlotCount);
        }

        private IPlayerPresenter CreateView(IPlayerConfig config)
        {
            var view = GameObject.Instantiate(config.PlayerViewPrefab.GameObject, Vector3.zero, Quaternion.identity)
                .GetComponent<PlayerView>();

            _camera.CinemachineCamera.Follow = view.transform;

            return new PlayerPresenter(view, _camera, _messageBus);
        }

        private void CreateServices(PlayerModel model, IPlayerPresenter presenter, IInventoryPresenter inventoryPresenter)
        {
            var moveService = new PlayerMoveService(_messageBus, model, presenter);
            var inventoryService = new PlayerInventoryService(model, inventoryPresenter, _itemFactory, presenter);
            var pickupService = new PlayerPickupItemService(_messageBus, inventoryService);
        }
    }
}