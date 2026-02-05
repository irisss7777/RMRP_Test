using Contracts.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.UI
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private UIDocument _uiDocument;
        public UIDocument UiDocument => _uiDocument;
    }
}