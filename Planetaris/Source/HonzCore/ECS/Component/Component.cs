using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS.Component
{
    public abstract class Component
    {
        public bool isEnabled;
        public bool isDestroyed;

        public bool isCreated;

        public void Destroy()
        {
            OnDestroy();
            isDestroyed = true;
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

        ~Component()
        {
            if (!isDestroyed)
            {
                Destroy();
            }
        }
    }
}
