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
            //나중에 생성자에서 인스펙터 그려서 돌려준다.
        }

        public void UpdateSelect(NodeView nodeView)
        {
            Clear(); //기존에 만들어진 화면 지우고
            UnityEngine.Object.DestroyImmediate(_editor);

            _editor = Editor.CreateEditor(nodeView.node);

            IMGUIContainer container = new IMGUIContainer(() => _editor.OnInspectorGUI());

            Add(container);
        }
    }
}
