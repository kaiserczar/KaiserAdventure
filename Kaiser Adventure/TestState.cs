using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;

namespace Kaiser_Adventure {

    class TestState : GameState {

        private int stateNum;
        private int counter;
        private SpriteBatch spriteBatch;

        public TestState(Main main, int stateNum) : base(main) {
            this.stateNum = stateNum;
            this.counter = 0;
        }

        public override void Update(GameTime gameTime) {
            counter++;
            if (counter>=300) {
                if (stateNum < 3) {
                    GameState.Push(new TestState(main, stateNum + 1));
                } else {
                    GameState.Pop();
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            int midX = main.GraphicsDevice.Viewport.Width / 2;
            int midY = main.GraphicsDevice.Viewport.Height / 2;

            this.spriteBatch.Begin();

            this.spriteBatch.DrawString(main.font, "State "+stateNum.ToString()+": "+ counter.ToString(), new Vector2(midX, midY), Color.Red);

            this.spriteBatch.End();
        }

        protected override void OnEnter() {
            this.counter = 0;
            this.spriteBatch = new SpriteBatch(main.GraphicsDevice);
        }

        protected override void OnExit() {
            this.spriteBatch.Dispose();
        }
    }
}
