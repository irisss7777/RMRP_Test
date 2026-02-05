using System.Collections.Generic;
using Contracts.Database;
using Contracts.Domain;
using Contracts.Factory;
using Contracts.Presentation;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.Controllers;
using Presentation.Presenters.Inventory;
using UnityEngine.UIElements;
using Zenject;

namespace Infrastructure.Factory.Inventory
{
    public class InventoryFactory
    {
        [Inject] private readonly IInventoryView _inventoryView;
        [Inject] private readonly MessageBus _messageBus;
        
        private const string SlotIconName = "slot-icon";
        private const string SlotLabelName = "slot-name";
        private const string SlotCountName = "slot-count";

        private const string InventorySlotClassName = "inventory-slot";
        private const string ItemIconClassName = "item-icon";
        private const string ItemLabelClassName = "item-name";
        private const string ItemCountClassName = "item-count";
        private const string ItemGridClassName = "ItemsGrid";
 
        public InventoryPresenter CreateInventory(IPlayerConfig config)
        {
            var slotsList = SetupDocument(config.InventorySlotCount);

            var presenter = new InventoryPresenter(_inventoryView, _messageBus, slotsList, SlotIconName, SlotLabelName, SlotCountName);

            presenter.Initialize();

            return presenter;
        }

        private List<VisualElement> SetupDocument(int slotsCount)
        {
            var root = _inventoryView.UiDocument.rootVisualElement;

            List<VisualElement> slots = new();

            var itemGrid = root.Q<VisualElement>(ItemGridClassName);

            itemGrid.Clear();

            for (var i = 1; i <= slotsCount; i++) CreateSlot(slots, itemGrid, i);

            return slots;
        }

        private void CreateSlot(List<VisualElement> slots, VisualElement itemGrid, int id)
        {
            var itemSlot = new VisualElement();
            itemSlot.AddToClassList(InventorySlotClassName);

            var itemIcon = new VisualElement();
            itemIcon.style.display = DisplayStyle.None;
            itemIcon.name = SlotIconName;
            itemIcon.AddToClassList(ItemIconClassName);

            var itemLabel = new Label();
            itemLabel.style.display = DisplayStyle.None;
            itemLabel.pickingMode = PickingMode.Ignore;
            itemLabel.name = SlotLabelName;
            itemLabel.AddToClassList(ItemLabelClassName);
            
            var itemCount = new Label();
            itemCount.style.display = DisplayStyle.None;
            itemCount.pickingMode = PickingMode.Ignore;
            itemCount.name = SlotCountName;
            itemCount.AddToClassList(ItemCountClassName);

            itemSlot.Add(itemIcon);
            itemSlot.Add(itemLabel);
            itemSlot.Add(itemCount);

            itemGrid.Add(itemSlot);
            slots.Add(itemSlot);
        }
    }
}