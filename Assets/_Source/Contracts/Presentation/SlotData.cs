using UnityEngine.UIElements;

namespace Contracts.Presentation
{
    public struct SlotData
    {
        public VisualElement Icon { get; private set; }
        public Label Label { get; private set; }
        public Label Count { get; }

        public SlotData(VisualElement icon, Label label, Label count)
        {
            Icon = icon;
            Label = label;
            Count = count;
        }
    }
}