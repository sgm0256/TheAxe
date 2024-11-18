using System;
using UnityEngine;

namespace Core.InteractiveObjects
{
    [Serializable]
    public class InteractiveOverride
    {
        // TODO : StatOverride랑 많이 겹치는 데 이거 구조 잡아 보셈
        [SerializeField] private InteractiveObjectInfoSO _info;
        [SerializeField] private bool _isUseOverride;
        [SerializeField] private float _overrideBaseValue;

        public InteractiveOverride(InteractiveObjectInfoSO info) => _info = info;
        
        public InteractiveObjectInfoSO CreateInfo()
        {
            InteractiveObjectInfoSO newInfo = _info.Clone() as InteractiveObjectInfoSO;

            if (_isUseOverride)
                newInfo.BaseValue = _overrideBaseValue;
            return newInfo;
        }
    }
} 