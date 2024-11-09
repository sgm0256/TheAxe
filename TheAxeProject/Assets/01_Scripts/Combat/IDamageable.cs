using Core.Entities;
using UnityEngine;

public enum DamageType
{
    Melee,
    Projectile,
    Range,
}

public interface IDamageable
{
    public void ApplyDamage(
        int damage, Vector3 hitPoint, Entity dealer, DamageType damageType);
}
