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
        
        internal bool isDestroyed;
        internal bool isCreated;

        public GameObject gameObject { get; private set; }


        private bool _isEnabled = true;

        public void Destroy()
        {

            isDestroyed = true;
            isEnabled = false;
            SetParent(null);
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
        internal void SetParent(GameObject gameObject)
        {
            this.gameObject = gameObject;
            if (gameObject == null)
                OnRemoveFromParent();
            else
                OnAddToParent();
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
        public virtual void OnAddToParent()
        {

        }
        public virtual void OnRemoveFromParent()
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
