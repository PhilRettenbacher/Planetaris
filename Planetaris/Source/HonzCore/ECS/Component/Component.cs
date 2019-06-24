using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS.Component
{
    public abstract class Component
    {
        public bool isEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; if (value) OnEnable(); else OnDisable(); }
        }
        
        public bool isDestroyed;
        public bool isCreated;

        private bool _isEnabled;

        public void Destroy()
        {
            isDestroyed = true;
            OnDestroy();      
        }

        public void CallCreate()
        {
            if (!isCreated)
            {
                isCreated = true;
                OnCreate();
            }
        }

        public virtual void Update()
        {

        }
        public virtual void Draw()
        {

        }
        public virtual void OnEnable()
        {

        }
        public virtual void OnDisable()
        {

        }
        public virtual void OnCreate()
        {

        }
        public virtual void OnDestroy()
        {

        }
        public virtual void OnAddToScene()
        {

        }
        public virtual void OnRemoveFromScene()
        {

        }

        public abstract Component Clone();

        ~Component()
        {
            if (!isDestroyed)
            {
                Destroy();
            }
        }
    }
}
