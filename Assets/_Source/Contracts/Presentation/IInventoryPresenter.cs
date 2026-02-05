using System;
using System.Collections.Generic;
using Contracts.Domain;
using UnityEngine.UIElements;

namespace Contracts.Presentation
{
    public interface IInventoryPresenter
    {
        public IInventoryView InventoryView { get; }
        public List<VisualElement> Slots { get; }
        public Dictionary<int, IInventorySlot> SlotItems { get; }

        public event Action<int, int> OnItemMoved;
        public event Action<int> OnItemDropped;

        public void AddItem(int slot, IInventorySlot inventorySlot);
        public void DropItem(int slot);
        public void MoveItem(int fromSlot, int toSlot);
        public void UpdateSlotView(int slot);
    }
}