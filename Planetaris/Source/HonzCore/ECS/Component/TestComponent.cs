using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS.Component
{
    class TestComponent : Component
    {
        public override Component Clone()
        {
            Console.WriteLine("Clone");
            return new TestComponent();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Console.WriteLine("Start");
        }
        public override void Update()
        {
            base.Update();
            Console.WriteLine("Update");
        }
    }
}
