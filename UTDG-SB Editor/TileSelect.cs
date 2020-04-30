using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTDG_SB_Editor.Handlers;

namespace UTDG_SB_Editor
{
    class TileSelect : MonoGameControl
    {
        public enum Layers
        {
            Tilemap,
            Collision,
            Depth,
            Entity
        }
        private Layers currentLayer = Layers.Tilemap;
        
        public readonly int tileSize = 64;
        private readonly int screenWidth = 3;
        private int maxScrollVal;

        public TextureHandler textureHandler;
        private Texture2D mouseIntersectTexture;
        private Texture2D selectedTileTexture;

        private Texture2D currentTexture;

        private Texture2D tileMap;
        private Texture2D collisionMap;
        private Texture2D depthMap;
        private Texture2D entityMap; 

        private Rectangle currentBounds;
        private Rectangle selectedTileBounds;
        public int selectedTile = 0;

        private Form form;
        private MG_Main mainWindow;

        private float scrollValue = 0f;

        public void ChangeLayer(Layers layer)
        {
            currentLayer = layer;
            switch (layer)
            {
                case Layers.Tilemap:
                    currentTexture = tileMap;
                    maxScrollVal = (currentTexture.Width / 16 / screenWidth) * tileSize + tileSize - Size.Height;
                    break;
                case Layers.Collision:
                    currentTexture = collisionMap;
                    maxScrollVal = 0;
                    break;
                case Layers.Depth:
                    currentTexture = depthMap;
                    maxScrollVal = 0;
                    break;
                case Layers.Entity:
                    currentTexture = entityMap;
                    maxScrollVal = 0;
                    break;
            }
            scrollValue = 0f;
            selectedTile = 0;
            selectedTileBounds = new Rectangle(0, 0, tileSize, tileSize);
        }

        private Matrix scrollMatrix
        {
            get{
                return Matrix.CreateTranslation(new Vector3(new Vector2(0, -scrollValue), 0));
            }
        }

        private Vector2 GetRealMousePos()
        {
            return Vector2.Transform(new Vector2(Editor.GetRelativeMousePosition.X, Editor.GetRelativeMousePosition.Y), Matrix.Invert(scrollMatrix));
        }
        
        public TileSelect()
        {
            textureHandler = new TextureHandler();
        }

        protected override void Initialize()
        {
            base.Initialize();
            textureHandler.LoadContent(this);

            tileMap = textureHandler.tileMapTexture;
            collisionMap = textureHandler.collisionMap;
            depthMap = textureHandler.depthMap;
            entityMap = textureHandler.entityMap;

            currentTexture = tileMap;

            maxScrollVal = (currentTexture.Width / 16 / screenWidth) * tileSize + tileSize - Size.Height;

            Color[] az = Enumerable.Range(0, 1).Select(i => Color.Red).ToArray();
            mouseIntersectTexture = textureHandler.colorTexture;
            selectedTileTexture = textureHandler.colorTexture;
            mouseIntersectTexture.SetData(az);
            selectedTileTexture.SetData(az);

            form = FindForm();
            mainWindow = form.Controls["MG_window"] as MG_Main;
        }

        public void ChangeTile(int newTile)
        {

            if (newTile < currentTexture.Width / 16)
            {
                selectedTile = newTile;
                selectedTileBounds = new Rectangle((newTile % screenWidth) * tileSize, (newTile / screenWidth) * tileSize, tileSize, tileSize);
                mainWindow.ChangeTile(newTile);
            }
            //switch (currentLayer)
            //{
            //    case Layers.Tilemap:
                    
            //        break;
            //    case Layers.Collision:

            //        break;
            //    case Layers.Depth:

            //        break;
            //}            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 Mousepos = GetRealMousePos();
            currentBounds = new Rectangle(((int)Mousepos.X / tileSize) * tileSize, ((int)Mousepos.Y / tileSize) * tileSize, tileSize, tileSize);            
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Vector2 mousePos = GetRealMousePos();
            int newTile = ((int)mousePos.X / tileSize) + ((int)mousePos.Y / tileSize) * screenWidth; 
            ChangeTile(newTile);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if ((e.Delta < 0 && scrollValue > 0) || (e.Delta > 0 && scrollValue < maxScrollVal))
                scrollValue += (e.Delta/5);

            if (scrollValue < 0) scrollValue = 0;
            else if (scrollValue > maxScrollVal) scrollValue = maxScrollVal;
        }        

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, scrollMatrix);

            //switch (currentLayer)
            //{
            //    case Layers.Tilemap:
                    //draw tiles
                    for (int i = 0; i < (currentTexture.Width / 16); i++)
                    {
                        int xPos = (i % screenWidth) * tileSize;
                        int yPos = (i / screenWidth) * tileSize;
                        Editor.spriteBatch.Draw(currentTexture, new Rectangle(xPos, yPos, tileSize, tileSize), new Rectangle(i * 16, 0, 16, 16), Color.White);
                    }
            //        break;
            //    case Layers.Collision:

            //        break;
            //    case Layers.Depth:

            //        break;
            //}


            //draw selected
            Editor.spriteBatch.Draw(selectedTileTexture, selectedTileBounds, Color.Yellow * 0.5f);

            //draw hover box
            if (IsMouseInsideControl)
            {
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X, currentBounds.Y, currentBounds.Width, 2), Color.Red);
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X + currentBounds.Width - 2, currentBounds.Y, 2, currentBounds.Height), Color.Red);
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X, currentBounds.Y + currentBounds.Height - 2, currentBounds.Width, 2), Color.Red);
                Editor.spriteBatch.Draw(mouseIntersectTexture, new Rectangle(currentBounds.X, currentBounds.Y, 2, currentBounds.Height), Color.Red);
            }

            Editor.spriteBatch.End();
        }
    }    
}
