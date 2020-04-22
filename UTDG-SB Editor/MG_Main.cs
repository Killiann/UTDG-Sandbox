using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UTDG_SB_Editor.Handlers;

namespace UTDG_SB_Editor
{
    public class MG_Main : MonoGameControl
    {
        public enum Layers
        {
            Tilemap,
            Collision,
            Depth
        }
        public Layers currentLayer = Layers.Tilemap;

        private Camera camera;

        private readonly int tileSize = 64;
        private int tmWidth = 15;
        private int tmHeight = 15;

        public TextureHandler textureHandler;
        private Texture2D mouseIntersectTexture;
        private Texture2D tileTexture;
        private Texture2D collisionTexture;
        private Texture2D depthTexture;

        private Rectangle currentBounds;

        private MainForm form;

        private UndoRedoHandler tileMapEditHandler;
        int[] tileMap;
        int[] collisionMap;
        int[] depthMap;

        int currentTileId = 0;

        private bool isClicking = false;
        private bool isSpaceDownOnClick = false;
        private Vector2 clickPos;

        public void ChangeTile(int newTile) { currentTileId = newTile; }
        private KeyboardState prevKeyboard;

        private Vector2 GetRealMousePos()
        {
            return Vector2.Transform(new Vector2(Editor.GetRelativeMousePosition.X, Editor.GetRelativeMousePosition.Y), Matrix.Invert(camera.translationMatrix));
        }

        public MG_Main()
        {
            textureHandler = new TextureHandler();
        }

        protected override void Initialize()
        {
            base.Initialize();
            textureHandler.LoadContent(this);            

            Color[] az = Enumerable.Range(0, 1).Select(i => Color.Red).ToArray();
            mouseIntersectTexture = textureHandler.colorTexture;
            mouseIntersectTexture.SetData(az);

            collisionTexture = textureHandler.collisionMap;
            depthTexture = textureHandler.depthMap;
            tileTexture = textureHandler.tileMapTexture;
            camera = new Camera(Editor.graphics.Viewport);

            form = (MainForm)FindForm();            

            tileMap = new int[tmWidth * tmHeight];
            collisionMap = new int[tmWidth * tmHeight];
            depthMap = new int[tmWidth * tmHeight];

            tileMapEditHandler = new UndoRedoHandler(tileMap);
            tileMapEditHandler.AddIteration(tileMap);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 Mousepos = GetRealMousePos();
            KeyboardState keyboard = Keyboard.GetState();

            currentBounds = new Rectangle(((int)Mousepos.X / tileSize) * tileSize, ((int)Mousepos.Y / tileSize) * tileSize, tileSize, tileSize);

            //undo/redo
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) 
                && keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z) 
                && prevKeyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Z)
                && keyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                tileMapEditHandler.Undo(ref tileMap);

            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl)
                && keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift)
                && keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z)
                && prevKeyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Z))
                tileMapEditHandler.Redo(ref tileMap);

            prevKeyboard = keyboard;
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (IsMouseInsideControl)
            {

                isClicking = true;
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                    isSpaceDownOnClick = true;
                else
                {
                    ChangeTileAtMouse();
                    isSpaceDownOnClick = false;
                }
                clickPos = GetRealMousePos();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e); 
            isClicking = false;
            if(IsMouseInsideControl)
                tileMapEditHandler.AddIteration(tileMap);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isClicking)
            {                
                if (isSpaceDownOnClick)                
                    camera.ChangePosition(Vector2.Subtract(clickPos, GetRealMousePos()));                
                else 
                    ChangeTileAtMouse();                
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.ChangeZoom((float)e.Delta/1000);
        }

        private void ChangeTileAtMouse()
        {
            Vector2 mousePos = GetRealMousePos();
            int currentTilePos = ((int)mousePos.X / tileSize % tmWidth) + (int)mousePos.Y / tileSize * tmWidth;
            if (currentTilePos < tileMap.Length && currentTilePos >= 0)
            {
                switch (currentLayer) {
                    case Layers.Tilemap:
                        tileMap[currentTilePos] = currentTileId;
                        break;
                    case Layers.Collision:
                        collisionMap[currentTilePos] = currentTileId;
                        break;
                    case Layers.Depth:
                        depthMap[currentTilePos] = currentTileId;
                        break;
                }                
            }
        }

        public void SaveMap(string location)
        {
            File.WriteAllText(location, String.Empty);
            using (StreamWriter file =
            new StreamWriter(location))
            {
                //dimensions
                file.Write(tmWidth.ToString() + ',' + tmHeight.ToString() + '#');
                //tileMap
                for(int i = 0; i < tileMap.Length; i++)
                {
                    file.Write(tileMap[i]);
                    if (i != depthMap.Length - 1)
                        file.Write(',');
                }
                file.Write('#');
                //collison map
                for (int i = 0; i < collisionMap.Length; i++)
                {
                    file.Write(collisionMap[i]);
                    if (i != depthMap.Length - 1)
                        file.Write(',');
                }
                file.Write('#');
                //depth map
                for (int i = 0; i < depthMap.Length; i++)
                {
                    file.Write(depthMap[i]);
                    if(i != depthMap.Length -1)
                        file.Write(',');
                }
                file.Write('#');
            }
        }

        public void OpenMap(string location)
        {
            string text = File.ReadAllText(location);
            string[] split = text.Split('#');
            tmWidth = Convert.ToInt32(split[0].Split(',')[0]);
            tmHeight = Convert.ToInt32(split[0].Split(',')[0]);
                        
            tileMap = new int[tmWidth * tmHeight];
            collisionMap = new int[tmWidth * tmHeight];
            depthMap = new int[tmWidth * tmHeight];

            string[] tileMapTest = split[1].Split(',');
            tileMap = Array.ConvertAll(split[1].Split(','), int.Parse);
            collisionMap = Array.ConvertAll(split[2].Split(','), int.Parse);
            depthMap = Array.ConvertAll(split[3].Split(','), int.Parse);

            tileMapEditHandler = new UndoRedoHandler(tileMap);
            tileMapEditHandler.AddIteration(tileMap);
        }

        public void AddXToAll(int x)
        {
            for(int i = 0; i < tileMap.Length; i++)
            {
                if(tileMap[i] + x <= ((textureHandler.tileMapTexture.Width / 16) - 1) && tileMap[i] + x >= 0)                
                    tileMap[i] += x;                
            }
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.translationMatrix);

            for(int i = 0; i < tileMap.Length; i++)
            {
                int x = i % tmWidth;
                int y = i / tmWidth;
                Editor.spriteBatch.Draw(tileTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), new Rectangle(tileMap[i] * 16, 0, 16, 16), Color.White);
            }
            if (currentLayer == Layers.Collision)
            {
                for (int i = 0; i < collisionMap.Length; i++)
                {
                    int x = i % tmWidth;
                    int y = i / tmWidth;
                    Editor.spriteBatch.Draw(collisionTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), new Rectangle(collisionMap[i] * 16, 0, 16, 16), Color.White * 0.5f);
                }
            }
            else if (currentLayer == Layers.Depth)
            {
                for (int i = 0; i < depthMap.Length; i++)
                {
                    int x = i % tmWidth;
                    int y = i / tmWidth;
                    Editor.spriteBatch.Draw(depthTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), new Rectangle(depthMap[i] * 16, 0, 16, 16), Color.White * 0.3f);
                }
            }

            if (IsMouseInsideControl)
            {
                switch (currentLayer)
                {
                    case Layers.Tilemap: Editor.spriteBatch.Draw(tileTexture, currentBounds, new Rectangle(currentTileId * 16, 0, 16, 16), Color.White * 0.8f); break;
                    case Layers.Collision: Editor.spriteBatch.Draw(collisionTexture, currentBounds, new Rectangle(currentTileId * 16, 0, 16, 16), Color.White * 0.8f); break;
                    case Layers.Depth: Editor.spriteBatch.Draw(depthTexture, currentBounds, new Rectangle(currentTileId * 16, 0, 16, 16), Color.White * 0.8f); break;
                }                
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X, currentBounds.Y, currentBounds.Width, 2), Color.Red);
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X + currentBounds.Width - 2, currentBounds.Y, 2, currentBounds.Height), Color.Red);
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X, currentBounds.Y + currentBounds.Height - 2, currentBounds.Width, 2), Color.Red);
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X, currentBounds.Y, 2, currentBounds.Height), Color.Red);
            }
            Editor.spriteBatch.End();
        }
    }
}
