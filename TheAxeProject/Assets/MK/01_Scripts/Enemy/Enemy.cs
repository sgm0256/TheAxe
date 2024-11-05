using UnityEngine;

namespace MK.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        // TODO : Add StatSystem

        protected Rigidbody2D _myRigid;
        public Rigidbody2D MyRigid => _myRigid;
        
        protected virtual void Initialize()
        {
            _myRigid = GetComponent<Rigidbody2D>();
        }
        
        protected virtual void Awake()
        {
            Initialize();
        }
    }
}
