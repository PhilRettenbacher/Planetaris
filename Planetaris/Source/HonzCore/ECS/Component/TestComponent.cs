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
            Console.WriteLine("DeltaTime: " + Helpers.TimeHelper.instance.deltaTimeUpdate);
        }
        public override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("Enabled");
        }
        public override void OnDisable()
        {
            base.OnDisable();
            Console.WriteLine("Disabled");
        }
        public override void OnRemoveFromParent()
        {
            base.OnRemoveFromParent();
            Console.WriteLine("Removed from Parent");
        }
    }
}
