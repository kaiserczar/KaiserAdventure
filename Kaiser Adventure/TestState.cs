﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;
using Kaiser_Adventure.Entities;
using Kaiser_Adventure.Entities.Controllers;

namespace Kaiser_Adventure {

    class TestState : GameState {

        private int stateNum;
        private int counter;
        private SpriteBatch spriteBatch;
        private RainbowColorCycler colorCycle;
        private Camera2D camera;

        private Character player;
        private List<Character> npcs = new List<Character>();

        private PositionMatcher cameraLock;

        public TestState(Main main, int stateNum) : base(main) {
            this.stateNum = stateNum;
            this.counter = 0;
            this.colorCycle = new RainbowColorCycler(4.0);
            camera = new Camera2D(main.GraphicsDevice.Viewport);

            this.player = new PlayerCharacter();
            this.cameraLock = new PositionMatcher(player, camera);

            Random rand = new Random();

            for (int i=0; i<5; i++) {
                Character npc = new Character();
                npc.Position = new Vector2(rand.Next(main.GraphicsDevice.Viewport.Width), rand.Next(main.GraphicsDevice.Viewport.Height));
                npcs.Add(npc);
            }
        }

        public override void Update(GameTime gameTime) {
            colorCycle.Update(gameTime);
            camera.Update(gameTime);
            counter++;
            /*if (counter>=300) {
                if (stateNum < 3) {
                    GameState.Push(new TestState(main, stateNum + 1));
                } else {
                    GameState.Pop();
                }
            }*/
            player.Update(gameTime);
            cameraLock.Syncronize();
        }

        public override void Draw(GameTime gameTime) {
            int midX = main.GraphicsDevice.Viewport.Width / 2;
            int midY = main.GraphicsDevice.Viewport.Height / 2;

            this.spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

            //this.spriteBatch.DrawString(main.font, "State "+stateNum.ToString()+": "+ counter.ToString(), new Vector2(midX, midY), colorCycle.color);
            this.spriteBatch.DrawString(main.font, "Angle: " + player.Rotation.ToString(), new Vector2(10, 10), Color.Red);

            foreach (Character c in npcs) {
                c.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

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
