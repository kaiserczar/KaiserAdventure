using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Kaiser_Adventure.Utilities {
    abstract class GameState {

        public static Stack<GameState> states = new Stack<GameState>();
        private static List<GameState> always = new List<GameState>();
        protected Main main;

        public GameState(Main main) {
            this.main = main;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        protected virtual void OnEnterAlways() { }

        // Change to another gamestate.
        // Returns the previous gamestate object.
        public static void Push(GameState gs) {
            if (states.Count > 0) {
                GameState retVal = states.Peek();
                states.Push(gs);

                retVal.OnExit();
                gs.OnEnter();
            }
            else {
                states.Push(gs);
                gs.OnEnter();
            }
        }

        // Go back to the previous gamestate active before the current one.
        // Returns the gamestate just left.
        public static void Pop() {
            GameState current = states.Pop();

            current.OnExit();
            states.Peek().OnEnter();
        }

        public static void AddAlways(GameState gs) {
            always.Add(gs);
            gs.OnEnterAlways();
        }

        // Call once in your main Update method in Game.
        public static void ManagerUpdate(Main main, GameTime gameTime) {

            if (states.Count == 0) main.Exit();

            foreach (GameState gs in always) {
                gs.Update(gameTime);
            }

            states.Peek().Update(gameTime);

        }

        // Call once in your main Draw method in Game.
        public static void ManagerDraw(Main main, GameTime gameTime) {

            if (states.Count == 0) main.Exit();

            foreach (GameState gs in always) {
                gs.Draw(gameTime);
            }

            states.Peek().Draw(gameTime);

        }

        public static GameState GetCurrent() {
            return states.Peek();
        }

    }
}
