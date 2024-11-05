using Core.Entities;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;
    
    private Entity _owner;
    private int _currentHealth;

    public void Initialize(Entity entity)
    {
        _owner = entity;
        //_currentHealth = _owner.Stat.maxHealth.GetValue(); //최대체력으로 셋팅
    }


    public void ApplyDamage(int damage, Vector3 hitPoint, Vector3 normal, float knockbackPower, Entity dealer, DamageType damageType)
    {
        //if (_owner.isDead) return;
        
        /*_currentHealth = Mathf.Clamp(
                _currentHealth - damage, 0, _owner.Stat.maxHealth.GetValue());*/
        OnHitEvent?.Invoke();
        
        if(_currentHealth <= 0)
        {
            OnDeadEvent?.Invoke();
        }
    }

    public void ApplyDamage(int damage, Vector3 hitPoint, Entity dealer, DamageType damageType)
    {
        throw new System.NotImplementedException();
    }
}
