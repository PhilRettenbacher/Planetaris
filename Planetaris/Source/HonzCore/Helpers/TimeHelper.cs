using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HonzCore.Helpers
{
    class TimeHelper : IHelper
    {
        private static TimeHelper _instance;

        public static TimeHelper instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeHelper();
                }
                return _instance;
            }
        }
        private TimeHelper()
        {

        }

        public double deltaTimeUpdate
        {
            get; private set;
        }
        public double totalGameTimeUpdate
        {
            get; private set;
        }
        public double deltaTimeDraw
        {
            get; private set;
        }
        public double totalGameTimeDraw
        {
            get; private set;
        }

        public void Draw(GameTime time)
        {
            deltaTimeDraw = time.ElapsedGameTime.TotalSeconds;
            totalGameTimeDraw = time.TotalGameTime.TotalSeconds;
        }
        public void Initialize()
        {

        }
        public void Update(GameTime time)
        {
            deltaTimeUpdate = time.ElapsedGameTime.TotalSeconds;
            totalGameTimeUpdate = time.TotalGameTime.TotalSeconds;
        }
    }
}
