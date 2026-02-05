using Contracts.Factory;
using Infrastructure.Factory.Inventory;
using Infrastructure.Factory.Item;
using Infrastructure.Factory.Player;
using Utils;
using Zenject;

namespace Infrastructure.Installer
{
    public class BaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            UtilsBind();
            FactoryBind();
        }

        private void UtilsBind()
        {
            Container.Bind<UniqueIdService>().AsSingle();
        }

        private void FactoryBind()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<InventoryFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemFactory>().AsSingle();
        }
    }
}