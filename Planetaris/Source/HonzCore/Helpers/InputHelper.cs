using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HonzCore.Helpers
{
    class InputHelper : IHelper
    {

        private static InputHelper _instance;

        public static InputHelper instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputHelper();
                }
                return _instance;
            }
        }
        private InputHelper()
        {

        }
        KeyboardState previous;

        public void Draw(GameTime time)
        {

        }

        public void Initialize()
        {
            previous = Keyboard.GetState();
        }

        public bool GetKey(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
        public bool GetKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && previous.IsKeyUp(key);
        }
        public bool GetKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key) && previous.IsKeyDown(key);
        }

        public void Update(GameTime time)
        {
            previous = Keyboard.GetState();
        }
    }
}
