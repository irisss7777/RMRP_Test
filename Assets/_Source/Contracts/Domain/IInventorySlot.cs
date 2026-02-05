namespace Contracts.Domain
{
    public interface IInventorySlot
    {
        public IItem Item { get; }
        public int StackCount { get; }
        public int SlotId { get; }

        public void UpdateSlotId(int newId);
        public void ClearSlot();
    }
}