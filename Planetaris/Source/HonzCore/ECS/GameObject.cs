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
        public bool isEnabled;

        public bool isDestroyed;
        public bool isRoot;


        public bool isInScene;
        public bool isInActiveScene
        {
            get
            {
                return parentScene == HonzCore.Helpers.ApplicationHelper.instance.activeScene;
            }
        }

        public Scene parentScene;

        private List<Component.Component> components = new List<Component.Component>();
        private Component.Component[] _copiedComponents;
        private bool requireComponentUpdate = true;

        private List<GameObject> children = new List<GameObject>();
        private GameObject[] _copiedChildren;
        private bool requireChildrenUpdate = true;

        private GameObject parent;

        public GameObject()
        {
            transform = new Transform();
        }

        public void AddComponent(Component.Component comp)
        {
            components.Add(comp);
            if (isCreated)
                comp.CallCreate();
            requireComponentUpdate = true;
        }
        public void RemoveComponent(Component.Component comp)
        {
            components.Remove(comp);
            requireComponentUpdate = true;
        }

        public void Update()
        {
            if (!isEnabled)
                return;
            LoopThroughComponentsAndChildren((comp) => comp.Update(), (gm) => gm.Update());
        }
        public void Draw()
        {
            if (!isEnabled)
                return;
            LoopThroughComponentsAndChildren((comp) => comp.Draw(), (gm) => gm.Draw());
        }

        public void SetParent(GameObject parent)
        {
            if(isRoot)
            {
                throw new InvalidOperationException("Can't set the parent of a root Object!");
            }
            if(this.parent != null)
            {
                this.parent.children.Remove(this);
                this.parent.transform.children.Remove(transform);
                parentScene = null;
                this.parent.requireChildrenUpdate = true;
            }
            this.parent = parent;
            transform.parent = parent != null ? parent.transform : null;
            if(this.parent != null)
            {
                this.parent.children.Add(this);
                this.parent.transform.children.Add(transform);
                parentScene = this.parent.parentScene;
                this.parent.requireChildrenUpdate = true;
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

        private void OnAddToScene()
        {
            LoopThroughComponentsAndChildren((comp) => comp.OnAddToScene(), (gm) => gm.OnAddToScene());
        }
        private void OnRemoveFromScene()
        {
            LoopThroughComponentsAndChildren((comp) => comp.OnRemoveFromScene(), (gm) => gm.OnRemoveFromScene());
        }

        internal void CallCreate()
        {
            if(isCreated)
            {
                return;
            }

            LoopThroughComponentsAndChildren((comp) => comp.CallCreate(), (gm) => gm.CallCreate());

            isCreated = true;
        }

        public void Destroy()
        {
            isDestroyed = true;
            LoopThroughComponentsAndChildren((comp) => comp.Destroy(), (gm) => gm.Destroy(), false);
        }

        private void LoopThroughComponentsAndChildren(Action<Component.Component> compAction, Action<GameObject> childAction, bool returnOnDestroy = true)
        {
            UpdateCopiedComponentAndChildrenLists();
            foreach (Component.Component comp in _copiedComponents)
            {
                compAction(comp);
                if (isDestroyed && returnOnDestroy)
                {
                    return;
                }
            }
            UpdateCopiedComponentAndChildrenLists();
            foreach (GameObject child in _copiedChildren)
            {
                childAction(child);
                if (isDestroyed && returnOnDestroy)
                {
                    return;
                }
            }
        }
        private void UpdateCopiedComponentAndChildrenLists()
        {
            if(requireComponentUpdate)
            {
                _copiedComponents = components.ToArray();
                requireComponentUpdate = false;
            }
            if(requireChildrenUpdate)
            {
                _copiedChildren = children.ToArray();
                requireChildrenUpdate = false;
            }
        }

        public GameObject Clone()
        {
            if(isDestroyed)
            {
                throw new InvalidOperationException("Can't copy a destroyed Object!");
            }
            if(isRoot)
            {
                throw new InvalidOperationException("Can't copy a root Object!");
            }
            GameObject newGm = new GameObject();

            newGm.isEnabled = isEnabled;

            foreach(var comp in components)
            {
                newGm.AddComponent(comp.Clone());
            }
            foreach(var child in children)
            {
                child.Clone().SetParent(newGm);
            }
            return newGm;
        }

        ~GameObject()
        {
            if (!isDestroyed)
                Destroy();
        }
    }
}
