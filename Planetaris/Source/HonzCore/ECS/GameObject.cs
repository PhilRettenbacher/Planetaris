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

        public void AddToScene(Scene scene)
        {
            parentScene = scene;
            if(parent == null || parent.isRoot)
            {
                SetParent(scene.root);
            }
        }
        public void RemoveFromScene()
        {
            parentScene = null;
            if(parent.isRoot)
            {
                SetParent(null);
            }
        }

        public void SetParent(GameObject parent)
        {
            if(this.parent != null)
            {
                this.parent.children.Remove(this);
                this.parent.transform.children.Remove(transform);
            }
            this.parent = parent;
            transform.parent = parent != null ? parent.transform : null;
            if(this.parent != null)
            {
                this.parent.children.Add(this);
                this.parent.transform.children.Add(transform);
            }
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
