using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS
{
    public class GameObject
    {
        public Transform transform;

        public bool isCreated;
        public bool enabled;
        public bool isDestroyed;
        public bool isRoot;

        public bool isInScene;
        public bool isInActiveScene
        { get
            {
                return true; //TODO 
            }
        }

        public Scene parentScene;

        private List<Component.Component> components = new List<Component.Component>();
        private List<GameObject> children = new List<GameObject>();
        private GameObject parent;

        public GameObject()
        {
            transform = new Transform();
        }

        public void AddComponent(Component.Component comp)
        {
            components.Add(comp);
            if (!comp.isCreated)
                comp.CallCreate();
        }
        public void RemoveComponent(Component.Component comp)
        {
            components.Remove(comp);
        }

        public void Update()
        {
            foreach(Component.Component comp in components)
            {
                comp.Update();
            }
            foreach (GameObject child in children)
            {
                child.Update();
            }
        }
        public void Draw()
        {
            foreach (Component.Component comp in components)
            {
                comp.Draw();
            }
            foreach(GameObject child in children)
            {
                child.Draw();
            }
        }


        public void SetParent(GameObject parent)
        {
            if(this.parent != null)
            {
                this.parent.children.Remove(this);
                this.parent.transform.children.Remove(transform);
                parentScene = null;
            }
            this.parent = parent;
            transform.parent = parent != null ? parent.transform : null;
            if(this.parent != null)
            {
                this.parent.children.Add(this);
                this.parent.transform.children.Add(transform);
                parentScene = this.parent.parentScene;
            }

            if(!isCreated && parent != null && parent.isCreated && isInActiveScene)
            {
                CallCreate();
            }

            bool isInSceneNow = this.parent != null && this.parent.isInScene;
            if(isInSceneNow != isInScene)
            {
                if(isInSceneNow)
                {
                    OnAddToScene();
                }
                else
                {
                    OnRemoveFromScene();
                }
            }
        }

        public void OnAddToScene()
        {
            //TODO Implement Logic
        }
        public void OnRemoveFromScene()
        {
            //TODO Implement Logic
        }

        public void CallCreate()
        {
            if(isCreated)
            {
                return;
            }

            foreach(Component.Component comp in components)
            {
                comp.CallCreate();
            }

            foreach(GameObject child in children)
            {
                child.CallCreate();
            }

            isCreated = true;
        }

        public void Destroy()
        {
            isDestroyed = true;
            foreach(Component.Component comp in components)
            {
                comp.Destroy();
            }
            foreach(GameObject child in children)
            {
                child.Destroy();
            }
        }

        ~GameObject()
        {
            if (!isDestroyed)
                Destroy();

        }
    }
}
