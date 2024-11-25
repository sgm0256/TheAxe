using Core.Entities;
using MK.Enemy;
using UnityEngine;

public class EffectCollision : MonoBehaviour
{
    [SerializeField] private SkillDataSO skillData;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.GetCompo<EntityHealth>().ApplyDamage(skillData.special, null);
        }
    }
}
