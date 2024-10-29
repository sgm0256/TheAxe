using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace BTVisual
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        private Editor _editor;

        public InspectorView()
        {
            //���߿� �����ڿ��� �ν����� �׷��� �����ش�.
        }

        public void UpdateSelect(NodeView nodeView)
        {
            Clear(); //������ ������� ȭ�� �����
            UnityEngine.Object.DestroyImmediate(_editor);

            _editor = Editor.CreateEditor(nodeView.node);

            IMGUIContainer container = new IMGUIContainer(() => _editor.OnInspectorGUI());

            Add(container);
        }
    }
}
