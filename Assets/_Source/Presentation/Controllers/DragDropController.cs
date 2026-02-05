using Contracts.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Controllers
{
    public class DragDropController
    {
        private readonly IInventoryPresenter _presenter;
        private Vector2 _dragOffset;
        private int _dragSourceSlot = -1;
        private bool _isDragging;

        private StyleColor _defaultSlotColor;
        private readonly StyleColor _selectSlotColor = new Color(0.49f, 0.78f, 0.8f);

        public DragDropController(IInventoryPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Initialize(VisualElement root)
        {
            root.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            root.RegisterCallback<MouseUpEvent>(OnMouseUp);

            GetDefaultSlotColor();
        }

        private void GetDefaultSlotColor()
        {
            var slot = _presenter.Slots[0];
            _defaultSlotColor = slot.style.backgroundColor;
        }

        public void SetupSlotForDrag(int slotIndex, VisualElement icon)
        {
            icon.RegisterCallback<MouseDownEvent>(evt => OnItemMouseDown(evt, slotIndex));
        }

        private void OnItemMouseDown(MouseDownEvent evt, int slotIndex)
        {
           if (!_presenter.SlotItems.ContainsKey(slotIndex)) return;

            if (evt.button == 0)
            {
                _isDragging = true;
                _dragSourceSlot = slotIndex;
                SetSourceOpacity(slotIndex, 0.3f);
                evt.StopPropagation();
            }
            else if (evt.button == 1)
            {
                _presenter.DropItem(slotIndex);
                evt.StopPropagation();
            }
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            if (!_isDragging) return;

            var mousePos = evt.mousePosition;
            HighlightDropTarget(mousePos);
        }

        private void OnMouseUp(MouseUpEvent evt)
        {
            if (!_isDragging || _dragSourceSlot == -1) return;

            SetSourceOpacity(_dragSourceSlot, 1f);
            
            var targetSlot = GetSlotAtPosition(evt.mousePosition);
            if (targetSlot != -1 && targetSlot != _dragSourceSlot)
                _presenter.MoveItem(_dragSourceSlot, targetSlot);

            CleanupDrag();
            ClearAllHighlights();
            evt.StopPropagation();
        }

        private void SetSourceOpacity(int slotIndex, float opacity)
        {
            var slotElement = _presenter.Slots[slotIndex];
            var icon = slotElement.Q<VisualElement>(className: "item-icon");
            var label = slotElement.Q<Label>(className: "item-name");
            var count = slotElement.Q<Label>(className: "item-count");
            
            if (icon != null) icon.style.opacity = opacity;
            if (label != null) label.style.opacity = opacity;
            if (count != null) count.style.opacity = opacity;
        }

        private int GetSlotAtPosition(Vector2 position)
        {
            var root = _presenter.InventoryView.UiDocument.rootVisualElement;
            var element = root.panel.Pick(position);

            while (element != null)
            {
                var slotIndex = _presenter.Slots.IndexOf(element);
                if (slotIndex != -1) return slotIndex;
                element = element.parent;
            }

            return -1;
        }

        private void HighlightDropTarget(Vector2 position)
        {
            ClearAllHighlights();
            var targetSlot = GetSlotAtPosition(position);
            if (targetSlot != -1)
            {
                var slot = _presenter.Slots[targetSlot];
                slot.style.backgroundColor = _selectSlotColor;
            }
        }

        private void ClearAllHighlights()
        {
            foreach (var slot in _presenter.Slots)
            {
                slot.style.backgroundColor = _defaultSlotColor;
            }
        }

        private void CleanupDrag()
        {
            _isDragging = false;
            _dragSourceSlot = -1;
        }

        public void Dispose()
        {
            var root = _presenter.InventoryView.UiDocument.rootVisualElement;
            root.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            root.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        }
    }
}