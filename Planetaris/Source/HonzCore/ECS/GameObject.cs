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

        private bool isCreated;
        public bool isEnabled { get => _isEnabled; set => _isEnabled = value; }

        private bool _isEnabled = true;

        internal bool isDestroyed;
        internal bool isRoot;

        public string name;

        public bool isInScene { get; internal set; }
        public bool isInActiveScene
        {
            get
            {
                return parentScene == HonzCore.Helpers.ApplicationHelper.instance.activeScene;
            }
        }

        public Scene parentScene { get; private set; }

        private List<Component.Component> components = new List<Component.Component>();
        private Component.Component[] _copiedComponents;
        private bool requireComponentUpdate = true;

        private List<GameObject> children = new List<GameObject>();
        private GameObject[] _copiedChildren;
        private bool requireChildrenUpdate = true;

        private GameObject parent;

        public GameObject(string name = null)
        {
            this.name = name ?? "GameObject";
            transform = new Transform();
        }

        public void AddComponent(Component.Component comp)
        {
            if(comp.gameObject != null)
            {
                throw new InvalidOperationException("Can't add a Component to 2 GameObjects");
            }
            comp.SetParent(this);
            components.Add(comp);
            if (isCreated)
                comp.CallCreate();
            requireComponentUpdate = true;
        }
        public void RemoveComponent(Component.Component comp)
        {
            comp.SetParent(null);
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
            if(!isRoot)
                SetParent(null);
            isDestroyed = true;
            isEnabled = false;
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

        public GameObject FindChildren(string name, bool recursive = false, bool requireEnabled = false)
        {
            if (isDestroyed || (requireEnabled && !isEnabled))
                return null;
            foreach(var child in children)
            {
                if (child.isDestroyed || (requireEnabled && !child.isEnabled))
                    return null;
                if (child.name == name)
                    return child;
                if(recursive)
                {
                    GameObject gm = child.FindChildren(name, recursive, requireEnabled);
                    if (gm != null)
                        return gm;
                }
            }
            return null;
        }

        public GameObject Clone(string name = null)
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
            newGm.name = name ?? this.name;

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
