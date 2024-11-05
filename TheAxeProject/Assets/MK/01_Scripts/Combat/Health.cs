using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;
    
    private Entity _owner;
    private int _currentHealth;

    public void Initialize(Entity agent)
    {
        _owner = agent;
        //_currentHealth = _owner.Stat.maxHealth.GetValue(); //최대체력으로 셋팅
    }


    public void ApplyDamage(int damage, Vector3 hitPoint, Vector3 normal, float knockbackPower, Agent dealer, DamageType damageType)
    {
        //if (_owner.isDead) return;

        Vector3 textPosition = hitPoint + new Vector3(0, 1f, 0);
        
        //넉백은 나중에 여기서 처리

        if (knockbackPower > 0)
        {
            ApplyKnockback(normal * -knockbackPower);
        }

        /*_currentHealth = Mathf.Clamp(
                _currentHealth - damage, 0, _owner.Stat.maxHealth.GetValue());*/
        OnHitEvent?.Invoke();
        
        if(_currentHealth <= 0)
        {
            OnDeadEvent?.Invoke();
        }

    }

    private void ApplyKnockback(Vector3 force)
    {
        //_owner.MovementCompo.GetKnockback(force);
    }
}
