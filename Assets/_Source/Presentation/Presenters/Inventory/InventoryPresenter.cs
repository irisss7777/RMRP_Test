using System;
using System.Collections.Generic;
using Contracts.Domain;
using Contracts.Presentation;
using Contracts.Signals;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.Controllers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Presenters.Inventory
{
    public class InventoryPresenter : IMessageDisposable, IInventoryPresenter
    {
        private readonly DragDropController _dragDropController;
        private readonly MessageBus _messageBus;
        private readonly string _slotLabelName;
        private readonly string _slotCountName;
        private readonly string _slotIconName;
        
        private bool _inventoryState;
        
        public event Action<int, int> OnItemMoved;
        public event Action<int> OnItemDropped;
        public event Action OnDispose;

        public IInventoryView InventoryView { get; }
        public List<VisualElement> Slots { get; }
        public Dictionary<int, IInventorySlot> SlotItems { get; } = new();

        public InventoryPresenter(IInventoryView inventoryView, MessageBus messageBus, List<VisualElement> slots, string slotIconName, string slotLabelName, string slotCountName)
        {
            InventoryView = inventoryView;
            _messageBus = messageBus;
            Slots = slots;
            _slotIconName = slotIconName;
            _slotLabelName = slotLabelName;
            _slotCountName = slotCountName;
            _dragDropController = new DragDropController(this);
        }

        public void Initialize()
        {
            _messageBus.Subscribe((InputInventorySignal signal) => ToggleInventory(), this);
            ToggleInventory();
            _dragDropController.Initialize(InventoryView.UiDocument.rootVisualElement);
            SetupAllSlotsForDrag();
        }

        public void AddItem(int slot, IInventorySlot inventorySlot)
        {
            if (!IsValidSlot(slot)) return;
            
            SlotItems[slot] = inventorySlot;
            UpdateSlotView(slot);
        }

        public void RemoveItem(int slot, IInventorySlot inventorySlot)
        {
            if (!IsValidSlot(slot)) return;
            
            SlotItems.Remove(slot);
            UpdateSlotView(slot);
        }

        public void DropItem(int slot)
        {
            if (!IsValidSlot(slot) || !SlotItems.ContainsKey(slot)) return;

            SlotItems.Remove(slot);
            UpdateSlotView(slot);
 
            OnItemDropped?.Invoke(slot);
        }

        public void MoveItem(int fromSlot, int toSlot)
        {
            if (!IsValidMove(fromSlot, toSlot)) return;

            // Сохраняем предметы
            var fromItem = SlotItems[fromSlot];
            IInventorySlot toItem = null;
            bool hasToItem = SlotItems.ContainsKey(toSlot);
            if (hasToItem)
            {
                toItem = SlotItems[toSlot];
            }

            SlotItems.Remove(fromSlot);
            
            if (hasToItem)
            {
                SlotItems[toSlot] = fromItem;
                SlotItems[fromSlot] = toItem;
            }
            else
            {
                SlotItems[toSlot] = fromItem;
                SlotItems.Remove(fromSlot);
            }

            UpdateSlotView(fromSlot);
            UpdateSlotView(toSlot);

            OnItemMoved?.Invoke(fromSlot, toSlot);
        }

        public SlotData GetSlotElements(int slot)
        {
            var slotElement = Slots[slot];
            return new SlotData(
                slotElement.Q<VisualElement>(_slotIconName), 
                slotElement.Q<Label>(_slotLabelName),
                slotElement.Q<Label>(_slotCountName)
            );
        }

        private void ToggleInventory()
        {
            var root = InventoryView.UiDocument.rootVisualElement;
            root.style.display = _inventoryState ? DisplayStyle.Flex : DisplayStyle.None;
            _inventoryState = !_inventoryState;
            _messageBus.Publish(new UiStateSignal(_inventoryState));
        }

        private IInventorySlot GetItemInSlot(int slot)
        {
            return SlotItems.ContainsKey(slot) ? SlotItems[slot] : null;
        }

        public void UpdateSlotView(int slot)
        {
            if (!IsValidSlot(slot)) return;

            var slotData = GetSlotElements(slot);
            var item = GetItemInSlot(slot);

            if (item != null)
            {
                slotData.Icon.style.display = DisplayStyle.Flex;
                slotData.Label.style.display = DisplayStyle.Flex;
                slotData.Label.text = item.Item.Name;
                slotData.Icon.style.backgroundColor = item.Item.Color;
    
                UpdateStackCount(slotData, item);
            }
            else
            {
                slotData.Icon.style.display = DisplayStyle.None;
                slotData.Label.style.display = DisplayStyle.None;
                slotData.Icon.style.backgroundColor = Color.clear;
                slotData.Label.text = "";
                
                if (slotData.Count != null)
                {
                    slotData.Count.style.display = DisplayStyle.None;
                    slotData.Count.text = "";
                }
            }
        }
        
        private void UpdateStackCount(SlotData slotData, IInventorySlot item)
        {
            if (slotData.Count == null) return;
            
            if (item.Item.IsStackable && item.StackCount > 1)
            {
                slotData.Count.style.display = DisplayStyle.Flex;
                slotData.Count.text = item.StackCount.ToString();
            }
            else
            {
                slotData.Count.style.display = DisplayStyle.None;
                slotData.Count.text = "";
            }
        }

        private bool IsValidSlot(int slot)
        {
            return slot >= 0 && slot < Slots.Count;
        }

        private bool IsValidMove(int fromSlot, int toSlot)
        {
            return fromSlot != toSlot && SlotItems.ContainsKey(fromSlot);
        }

        private void SetupAllSlotsForDrag()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                var slotElement = Slots[i];
                var icon = slotElement.Q<VisualElement>(_slotIconName);
                if (icon != null)
                {
                    _dragDropController.SetupSlotForDrag(i, icon);
                }
            }
        }
        
        public void Dispose()
        {
            _dragDropController.Dispose();
            OnDispose?.Invoke();
        }
    }
}