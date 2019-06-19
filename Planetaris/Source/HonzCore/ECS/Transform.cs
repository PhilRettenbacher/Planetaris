using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame;

namespace HonzCore.ECS
{
    public class Transform
    {
        public Vector2 position;
        public float rotation;

        public GameObject gameObject;

        public Transform parent;
        public List<Transform> children = new List<Transform>();
    }
}
