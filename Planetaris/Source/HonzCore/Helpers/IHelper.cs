using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.Helpers
{
    public interface IHelper 
    {
        void Initialize();
        void Update(Microsoft.Xna.Framework.GameTime time);
        void Draw(Microsoft.Xna.Framework.GameTime time);
        
    }
}
