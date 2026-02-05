using Contracts.Domain;

namespace Contracts.Signals
{
    public struct SelectForPickupSignal
    {
        public bool CanPickup { get; private set; }
        public IItem ItemData { get; private set; }
        public int ItemViewId { get; private set; }

        public SelectForPickupSignal(bool canPickup, IItem itemData, int itemViewId)
        {
            CanPickup = canPickup;
            ItemData = itemData;
            ItemViewId = itemViewId;
        }
    }
}