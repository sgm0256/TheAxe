using Core.StatSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Test
{
    public class StatTester : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private StatSO _testStat;
        [SerializeField] private float _testValue = 3f;

        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                _player.GetCompo<EntityStat>().GetStat(_testStat).AddModifier("Test1", _testValue);
            }
            
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                _player.GetCompo<EntityStat>().GetStat(_testStat).RemoveModifier("Test1");
            }
        }
    }
}
