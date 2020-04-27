using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UTDG_DEV.Scene_Objects.Entities;

namespace UTDG_DEV.Handlers
{
    class PlayerInputHandler
    {
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;

        public PlayerInputHandler() {
            keyboard = Keyboard.GetState();
            prevKeyboard = keyboard;
        }        

        public void Update(Player player)
        {
            keyboard = Keyboard.GetState();
            Vector2 directionVector = new Vector2(0,0);

            //movement
            if (keyboard.IsKeyDown(Keys.W)) directionVector.Y = -1;
            if (keyboard.IsKeyDown(Keys.S)) directionVector.Y = 1;
            if (keyboard.IsKeyDown(Keys.A)) directionVector.X = -1;
            if (keyboard.IsKeyDown(Keys.D)) directionVector.X = 1;
            player.directionVector = directionVector;

            prevKeyboard = keyboard;
        }
    }
}
