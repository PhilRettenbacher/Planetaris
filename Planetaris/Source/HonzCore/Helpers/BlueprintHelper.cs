using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HonzCore.ECS;
using Microsoft.Xna.Framework;

namespace HonzCore.Helpers
{
    class BlueprintHelper : IHelper
    {
        Dictionary<string, GameObject> blueprints = new Dictionary<string, GameObject>();

        private static BlueprintHelper _instance;

        public static BlueprintHelper instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlueprintHelper();
                }
                return _instance;
            }
        }
        private BlueprintHelper()
        {

        }

        public void RegisterBlueprint(GameObject gm, string name = null)
        {
            if(gm == null)
            {
                throw new ArgumentException("The GameObject cannot be null");
            }
            blueprints.Add(name ?? gm.name, gm);
        }

        public GameObject CreateBlueprint(string key, GameObject parent = null, Scene parentScene = null)
        {
            if(key == null)
            {
                throw new ArgumentException("The index cannot be null");
            }
            GameObject newGm = blueprints[key].Clone();
            newGm.SetParent(parent ?? (parentScene.root ?? ApplicationHelper.instance.activeScene.root));
            return newGm;
        }

        public void Draw(GameTime time)
        {
            
        }
        public void Initialize()
        {
            
        }
        public void Update(GameTime time)
        {
            
        }
    }
}
