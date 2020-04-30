using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UTDG_DEV.Handlers;
using UTDG_DEV.Scene_Objects;

namespace UTDG_DEV
{    
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public TextureHandler textureHandler;
        MouseInputHandler mouseInputHandler;

        public readonly int TileSize = 32;

        public Scene currentScene;

        public Main()
        {                     
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
            };
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            textureHandler = new TextureHandler();
            base.Initialize();
            currentScene = new Scene(this);
            mouseInputHandler = new MouseInputHandler(this);
        }

        protected override void LoadContent()
        {         
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureHandler.LoadContent(this);
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mouseInputHandler.Update();
            currentScene.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentScene.Draw(graphics, spriteBatch);
            mouseInputHandler.Draw(spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}
