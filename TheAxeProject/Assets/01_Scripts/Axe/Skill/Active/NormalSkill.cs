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
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.GetCompo<EntityHealth>().ApplyDamage(damage + skillData.damage, axe);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Gizmos.DrawWireSphere(transform.position, radius);
    }
}
