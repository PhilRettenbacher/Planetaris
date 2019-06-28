using HonzCore.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS
{
    public class Scene
    {
        public GameObject root { get; private set; }
		//Master UIEntity for this Scene.
		public UIEntity uiSystem { get; private set; }

        public bool isActiveScene
        {
            get
            {
                return HonzCore.Helpers.ApplicationHelper.instance.activeScene == this;
            }
        }

        public Scene ()
        {
            root = new GameObject();
            root.isRoot = true;
            root.isInScene = true;

			uiSystem = new UIEntity();
			uiSystem.IsRoot = true;
			root.isInScene = true;
		}

        public void Update()
        {
            if(isActiveScene)
			{
                root.Update();
				uiSystem.Update();
			}
        }
        public void Draw()
        {
            if(isActiveScene)
			{
                root.Draw();
				uiSystem.Draw();
			}
        }

        public void Activate()
        {
            root.CallCreate();
			uiSystem.CallCreate();
        }
        public void Deactivate()
        {

        }

        public void Destroy()
        {

        }

        public GameObject FindGameObject(string name, bool recursive = false, bool requireEnabled = false)
        {
            return root.FindChildren(name, recursive, requireEnabled);
        }

        ~Scene()
        {

        }
    }
}
