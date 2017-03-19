using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Kaiser_Adventure.Utilities {

    class RainbowColorCycler {
        
        public Color color { get;  private set; }
        private double period;
        private double currentTime;

        public RainbowColorCycler(double period) {
            this.period = period;
            this.currentTime = 0;

        }

        public RainbowColorCycler(TimeSpan period) : this(period.TotalSeconds) { }

        public void Update(GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            double radianAngle = (((delta+currentTime) % period) / period) * 2 * Math.PI;

            float R = (float) Math.Sin(radianAngle);
            float G = (float) Math.Sin(radianAngle + 2 * Math.PI / 3);
            float B = (float) Math.Sin(radianAngle + 4 * Math.PI / 3);

            if (R < 0) R = 0;
            if (G < 0) G = 0;
            if (B < 0) B = 0;

            color = new Color(R, G, B);
            currentTime = (delta + currentTime) % period;
        }

    }
}
