using System;
using Contracts.Presentation;
using Contracts.Signals;
using Domain.Player;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Application.Player
{
    public class PlayerMoveService : IMessageDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly IPlayerPresenter _playerPresenter;

        private bool _canMove = true;

        public event Action OnDispose;

        public PlayerMoveService(MessageBus messageBus, PlayerModel playerModel, IPlayerPresenter playerPresenter)
        {
            _playerModel = playerModel;
            _playerPresenter = playerPresenter;
            
            messageBus.Subscribe((InputMoveSignal signal) => Move(signal), this);
            messageBus.Subscribe((UiStateSignal signal) => SetUiState(signal.IsUiState), this);
        }

        private void Move(InputMoveSignal signal)
        {
            if(!_canMove)
                return;
            
            _playerPresenter.Move(signal.MoveDirection, _playerModel.Speed);
        }

        private void SetUiState(bool isUiState)
        {
            _canMove = isUiState;
            
            _playerPresenter.SetCameraActive(isUiState);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}