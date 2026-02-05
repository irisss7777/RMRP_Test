using Contracts.Domain;

namespace Domain.Player.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public bool SlotIsEmpty => Item == null;
        public IItem Item { get; private set; }
        public int StackCount { get; private set; }
        public int SlotId { get; private set; }

        public InventorySlot(int slotId)
        {
            SlotId = slotId;
        }

        public void UpdateSlotId(int newId)
        {
            SlotId = newId;
        }

        public bool TryAddItem(IItem item)
        {
            if (!SlotIsEmpty && Item.Id != item.Id)
                return false;

            if (SlotIsEmpty)
            {
                AddItem(item.StackCount);
                Item = item;
            }
            else
            {
                if (!Item.IsStackable)
                    return false;

                if (StackCount >= Item.MaxStackCount)
                    return false;

                AddItem(item.StackCount);
            }

            return true;
        }

        private void AddItem(int itemCount)
        {
            StackCount += itemCount;
        }

        public void ClearSlot()
        {
            StackCount = 0;
            Item = null;
        }
    }
}