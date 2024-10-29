using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace BTVisual
{
    public class BTEditor : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _treeAsset = default;

        private BTView _treeView;
        private InspectorView _inspector;

        [MenuItem("Window/BTEditor")]
        public static void OpenWindow()
        {
            BTEditor wnd = GetWindow<BTEditor>();
            wnd.titleContent = new GUIContent("GGM BT Editor");
        }

        [OnOpenAsset]
        public static bool OnOpenAssetHandle(int instanceId, int line)
        {
            if(Selection.activeObject is BehaviourTree)
            {
                OpenWindow();
                return true;
            }
            return false;
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            VisualElement content = _treeAsset.Instantiate();
            root.Add(content);
            content.style.flexGrow = 1;

            _treeView = content.Q<BTView>("TreeView");
            _inspector = root.Q<InspectorView>("Inspector");

            _treeView.OnNodeSelected += HandleNodeSelect;

            //초기에 한번 갱신
            OnSelectionChange();

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTVisual/Editor/BTEditor.uss");
            root.styleSheets.Add(styleSheet);
        }

        private void HandleNodeSelect(NodeView nodeView)
        {
            _inspector.UpdateSelect(nodeView);
        }

        //선택 등 그래프의 변경이 일어났을 때 실행하는 함수
        private void OnSelectionChange()
        {
            var tree = Selection.activeObject as BehaviourTree;
            if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _treeView.PopulateView(tree);
            }
        }
    }
}
