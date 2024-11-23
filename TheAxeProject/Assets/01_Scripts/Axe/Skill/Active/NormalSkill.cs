using Core.Entities;
using Core.StatSystem;
using MK.Enemy;
using UnityEngine;

public class NormalSkill : Skill
{
    protected override void Impact()
    {
        base.Impact();

        
    }

    protected override void FlightSkill(GameObject obj)
    {
        float damage = GameManager.Instance.Player.GetCompo<EntityStat>().GetStat(damageStat).Value;
        damage += skillData.damage;

        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.GetCompo<EntityHealth>().ApplyDamage(damage, axe);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Gizmos.DrawWireSphere(transform.position, radius);
    }
}
