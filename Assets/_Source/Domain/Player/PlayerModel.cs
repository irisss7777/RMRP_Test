using Domain.Player.Inventory;

namespace Domain.Player
{
    public class PlayerModel
    {
        public int Id { get; private set; }
        public float Speed { get; private set; }
        public InventoryModel Inventory { get; private set; }
        
        public PlayerModel(float speed, InventoryModel inventory, int id)
        {
            Speed = speed;
            Inventory = inventory;
            Id = id;
        }
    }
}