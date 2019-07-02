using HonzCore.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.Helpers
{
    public class ApplicationHelper : IHelper
    {
        private static ApplicationHelper _instance;

        public static ApplicationHelper instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ApplicationHelper();
                }
                return _instance;
            }
        }
        private ApplicationHelper()
        {

        }

        public Scene activeScene
        {
            get;
            private set;
        }

        public void LoadScene(Scene newScene)
        {
            if(activeScene != null)
            {
                activeScene.Deactivate();
            }
            activeScene = newScene;
            if(activeScene != null)
            {
                activeScene.Activate();
            }
        }

        public void Initialize()
        {

        }
        public void Draw(GameTime time)
        {
            activeScene.Draw();
        }
        public void Update(GameTime time)
        {
            activeScene.Update();
        }
    }
}
