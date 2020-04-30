using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Scene_Objects.Entities;

namespace UTDG_DEV.Handlers
{
    class MouseInputHandler
    {
        MouseState prevMouseState;
        MouseState mouse;

        private Texture2D cursor;
        private Rectangle cursorBounds;

        Main game;
        public MouseInputHandler(Main game)
        {
            this.game = game;
            mouse = Mouse.GetState();
            prevMouseState = mouse;

            cursor = game.textureHandler.cursorTexture;
            cursorBounds = new Rectangle(0, 0, 8, 8);
        }

        public void Update()
        {
            mouse = Mouse.GetState();

            Vector2 mousePos = game.currentScene.camera.ScreenToWorld(new Vector2(mouse.Position.X, mouse.Position.Y));
            cursorBounds.Location = new Point((int)mousePos.X, (int)mousePos.Y);

            if (mouse.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < game.currentScene.LiveEntities.Count; i++)
                {
                    if (cursorBounds.Intersects(game.currentScene.LiveEntities[i].RenderBounds()))
                    {
                        if (game.currentScene.LiveEntities[i] is TrainingDummy) ((TrainingDummy)game.currentScene.LiveEntities[i]).Hit();
                        if(game.currentScene.LiveEntities[i] is Chest)
                        {
                            if (((Chest)game.currentScene.LiveEntities[i]).isOpen) ((Chest)game.currentScene.LiveEntities[i]).Close();
                            else ((Chest)game.currentScene.LiveEntities[i]).Open();
                        }
                    }
                }
            }
            prevMouseState = mouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, game.currentScene.camera.TranslationMatrix);
            spriteBatch.Draw(cursor, cursorBounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            spriteBatch.End();
        }
    }
}
