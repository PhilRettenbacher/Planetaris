using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS
{
    public class Scene
    {
        public GameObject root;

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
        }

        public void Update()
        {
            if(isActiveScene)
                root.Update();
        }
        public void Draw()
        {
            if(isActiveScene)
                root.Draw();
        }

        public void Activate()
        {
            root.CallCreate();
        }
        public void Deactivate()
        {

        }

        public void Destroy()
        {

        }

        ~Scene()
        {

        }
    }
}
