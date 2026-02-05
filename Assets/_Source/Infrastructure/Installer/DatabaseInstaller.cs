using Contracts.Database;
using Infrastructure.Database.Configs;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    [CreateAssetMenu(fileName = "DatabaseInstaller", menuName = "Installers/DatabaseInstaller")]
    public class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller>
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private ItemsConfig _itemsConfig;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerConfig>().FromInstance(_playerConfig).AsSingle();
            Container.Bind<IItemsConfig>().FromInstance(_itemsConfig).AsSingle();
        }
    }
}