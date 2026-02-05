using Contracts.Signals;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Presentation.Controllers
{
    public class InputReader : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;

        private InputSystem_Actions _inputSystemActions;

        private void OnEnable()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Enable();

            Subscribe();
        }

        private void Update()
        {
            ReadMove();
        }

        private void Subscribe()
        {
            _inputSystemActions.UI.Inventory.started += ReadInventoryInput;
            _inputSystemActions.Player.PickupItem.started += ReadPickupItemInput;
        }

        private void ReadMove()
        {
            var direction = _inputSystemActions.Player.Move.ReadValue<Vector2>();

            _messageBus.Publish(new InputMoveSignal(direction));
        }
        
        private void ReadInventoryInput(InputAction.CallbackContext context)
        {
            _messageBus.Publish(new InputInventorySignal());
        }
        
        private void ReadPickupItemInput(InputAction.CallbackContext context)
        {
            _messageBus.Publish(new InputPickupItemSignal());
        }

        private void OnDisable()
        {
            _inputSystemActions.Disable();
            _inputSystemActions.Dispose();

            _inputSystemActions.UI.Inventory.started -= ReadInventoryInput;
            _inputSystemActions.Player.PickupItem.started -= ReadPickupItemInput;
        }
    }
}