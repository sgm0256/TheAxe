using Core.Entities;
using Core.StatSystem;
using MK.Enemy;
using ObjectPooling;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected PlayerManagerSO playerSO;
    [SerializeField] protected float radius = 0.5f;
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected PoolTypeSO effectPoolType;

    public SkillDataSO skillData;
    public StatSO damageStat;

    protected Axe axe;
    protected AxeMover mover;
    protected EntityStat stat;

    protected float damage => playerSO.Player.GetCompo<EntityStat>().GetStat(damageStat).Value;

    public virtual void Awake()
    {
        axe = GetComponentInParent<Axe>();
        mover = axe.GetCompo<AxeMover>();

        //axe.OnAxeImpact += Impact;

        transform.root.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (stat == null)
            stat = playerSO.Player.GetCompo<EntityStat>();
    }

    protected virtual void Impact(Vector3 lastDir)
    {
        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(axe);
    }

    public virtual void StartSkill()
    {
        mover.AttackMove();
    }

    protected virtual void FlightSkill(GameObject obj) { }

    public int GetLevel()
    {
        return skillData.level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!axe.isAttack)
            return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (skillData.isFlight)
            {
                FlightSkill(collision.gameObject);
            }
            else
            {
                enemy.GetCompo<EntityHealth>().ApplyDamage(damage, axe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, skillData.baseRange / 2);
    }
}
