using System;
using Core.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private Image _gauage;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private float _colorChangeSpeed;
    [SerializeField] private PlayerManagerSO _playerManager;
    
    private EntityLevel _level;
    private Color _oringinColor;
    private float _currentExp = 0;
    private int _currentLevelUpNeedValue = 0;
    private int _currentLevel = 0;
    private bool _isRainbowGauage = false;
    private float _hue;

    private void Start()
    {
        _level = _playerManager.Player.GetCompo<EntityLevel>();
        
        _gauage.fillAmount = 0;
        _levelText.text = $"LV 0";
        _currentLevelUpNeedValue = _level.LevelUpNeedValue;
        
        _level.LevelUpEvent                     += HandleLevelUp;
        _level.OnGetExpEvent                    += HandleGetExp;
        _upgradeManager.OnStartSelectSkillEvent += HandleStartSelect;
        _upgradeManager.OnSelectSkillEvent      += HandleSelectSkill;
        _oringinColor = _gauage.color;
    }

    private void OnDestroy()
    {
        _upgradeManager.OnStartSelectSkillEvent -= HandleStartSelect;
        _upgradeManager.OnSelectSkillEvent      -= HandleSelectSkill;
    }

    private void HandleGetExp(float exp)
    {
        _currentExp += exp;
        _gauage.fillAmount = _currentExp / _currentLevelUpNeedValue;
    }

    private void HandleLevelUp(int level)
    {
        _currentLevel = level;
        _currentExp = 0;
        _currentLevelUpNeedValue = _level.LevelUpNeedValue;
    }
    
    private void HandleStartSelect()
    {
        _gauage.fillAmount = 1;
        _isRainbowGauage = true;
    }
    
    private void HandleSelectSkill()
    {
        _isRainbowGauage = false;
        _gauage.color = _oringinColor;
        _gauage.fillAmount = 0;
        _levelText.text = $"LV {_currentLevel}";
    }

    private void Update()
    {
        if (_isRainbowGauage)
        {
            _hue = (_hue + _colorChangeSpeed * Time.unscaledDeltaTime) % 1f;

            Color rainbowColor = Color.HSVToRGB(_hue, 1f, 1f);

            _gauage.color = rainbowColor;
        }
    }
}
