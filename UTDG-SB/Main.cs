using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using UTDG_SB.Dev;
using UTDG_SB.Entities;
using UTDG_SB.Handlers;
using UTDG_SB.Map;

namespace UTDG_SB
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public TextureHandler textureHandler;
        public FontHandler fontHandler;
        public TileMap map;
        public Camera camera;
        public Player player;
        public TrainingDummy trainingDummy;

        public bool devMode = false;
        DevConsole devConsole;

        MouseState prevMouse = Mouse.GetState();
        KeyboardState prevKeyboard = Keyboard.GetState();

        Texture2D cursorTexture;
        Vector2 cursorPosition;

        Texture2D tempCrate;
        Rectangle tempCrateRect;

        Chest tempChest;

        //temp torch
        Texture2D torchTexture;
        Rectangle torchRect;
        int currentFrame = 0;
        int frameRate = 5;
        int currentFrameTime = 0;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Sandboxin 4 days";            
            Window.AllowUserResizing = true;

            Window.ClientSizeChanged += OnResize;
            Window.TextInput += TextInputHandler;

            graphics.PreferredBackBufferHeight = 450;
            textureHandler = new TextureHandler();
            fontHandler = new FontHandler();           
        }       

        protected override void Initialize()
        {
            base.Initialize();
            map = new TileMap(this);
            camera = new Camera(GraphicsDevice.Viewport);
            camera.SetPosition(new Vector2(160, 160));
            player = new Player(this);
            trainingDummy = new TrainingDummy(this, new Vector2(128, 64));
            devConsole = new DevConsole(this);

            tempCrateRect = new Rectangle(64, 64, 32, 32);
            torchRect = new Rectangle(64, 32, 32, 32);
            tempChest = new Chest(this);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureHandler.LoadContent(this);
            fontHandler.LoadContent(this);

            tempCrate = Content.Load<Texture2D>("images/crate");
            torchTexture = Content.Load<Texture2D>("images/torch");
        }

        public void ReloadContent()
        {
            Content.Unload();
            LoadContent();
        }

        //screen resize
        private void OnResize(Object sender, EventArgs e)
        {
            if (GraphicsDevice.Viewport.Width < 800)
            {
                graphics.PreferredBackBufferWidth = 800;
                graphics.ApplyChanges();
            }
            //keep aspect ratio
            graphics.PreferredBackBufferHeight = (int)((float)GraphicsDevice.Viewport.Width / 16 * 9); 
            graphics.ApplyChanges();
            camera.ResetViewPort(GraphicsDevice.Viewport);
        }

        private void TextInputHandler(Object sender, TextInputEventArgs e)
        {
            if (devConsole.IsOpen())
            {
                devConsole.HandleInput(e);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //dev log
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.F1) && prevKeyboard.IsKeyUp(Keys.F1))
                if (devConsole.IsOpen())
                    devConsole.Close();
                else devConsole.Open();

            if(keyboard.IsKeyDown(Keys.C) && prevKeyboard.IsKeyUp(Keys.C))
            {
                if (tempChest.IsOpen())
                    tempChest.Close();
                else if (!tempChest.IsOpen())
                    tempChest.Open();
            }

            devConsole.Update();
            //input-mouse

            MouseState mouse = Mouse.GetState();
            Vector2 realMousePos = camera.ScreenToWorld(new Vector2(mouse.Position.X, mouse.Position.Y));
            cursorPosition = realMousePos;

            if (!devConsole.IsOpen())
            {
                player.Update();
                trainingDummy.Update();

                if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                    if (trainingDummy.bounds.Intersects(new Rectangle((int)realMousePos.X, (int)realMousePos.Y, 8, 8)))
                        trainingDummy.Hit();

                if (player.bounds.Y < trainingDummy.GetPosition().Y)
                    trainingDummy.depth = 0.4f;
                else trainingDummy.depth = 0.7f;
            }

            if (currentFrameTime > frameRate)
            {
                if (currentFrame < (torchTexture.Width / 16) - 1)
                    currentFrame++;
                else currentFrame = 0;

                currentFrameTime = 0;
            }
            else currentFrameTime++;

            tempChest.Update();
            camera.SetPosition(player.GetPosition());
            prevMouse = mouse;
            prevKeyboard = keyboard;
        }

        protected override void Draw(GameTime gameTime)
        {            
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, 
                SamplerState.PointClamp, null, null, null, camera.TranslationMatrix);
            map.Draw(spriteBatch);
            trainingDummy.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.Draw(tempCrate, tempCrateRect, Color.White);
            tempChest.Draw(spriteBatch);

            spriteBatch.Draw(torchTexture, torchRect, new Rectangle(currentFrame * 16, 0, 16, 16) ,Color.White);
            spriteBatch.Draw(torchTexture, new Rectangle(160, 32, 32, 32) , new Rectangle(currentFrame * 16, 0, 16, 16), Color.White);

            spriteBatch.Draw(textureHandler.cursorTexture, new Rectangle((int)cursorPosition.X, (int)cursorPosition.Y, 8, 8), Color.White);
            spriteBatch.End();

            //UI
            spriteBatch.Begin(SpriteSortMode.Deferred);

            //Dev
            devConsole.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
