namespace Contracts.Signals
{
    public struct UiStateSignal
    {
        public bool IsUiState;

        public UiStateSignal(bool isUiState)
        {
            IsUiState = isUiState;
        }
    }
}