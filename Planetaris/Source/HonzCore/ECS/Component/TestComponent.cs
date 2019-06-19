using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.ECS.Component
{
    class TestComponent : Component
    {
        public override void Update()
        {
            base.Update();
            Console.WriteLine("Update");
        }
    }
}
