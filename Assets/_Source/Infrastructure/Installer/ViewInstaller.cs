using Contracts.Presentation;
using Presentation.UI;
using Presentation.View.Camera;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _camera;
        [SerializeField] private InventoryView _inventoryView;

        public override void InstallBindings()
        {
            CameraBind();
            InventoryBind();
        }

        private void CameraBind()
        {
            Container.Bind<ICameraView>().FromInstance(_camera).AsSingle();
        }

        private void InventoryBind()
        {
            Container.Bind<IInventoryView>().FromInstance(_inventoryView).AsSingle();
        }
    }
}