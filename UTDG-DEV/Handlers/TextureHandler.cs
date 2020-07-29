using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace UTDG_DEV.Handlers
{
    public class TextureHandler
    {
        //tilemaps
        public Texture2D dungeonTileMapTexture;

        //player
        public Texture2D player_idle;
        public Texture2D player_idle_copy;
        public Texture2D player_moveUp;
        public Texture2D player_moveX;
        public Texture2D player_radius;

        //training dummy
        public Texture2D dummy_idle;
        public Texture2D dummy_hit;

        //Lights
        public Texture2D torch;

        //Containers
        public Texture2D chest;
        public Texture2D chest_opening;

        //Items
        public Texture2D rayGun;

        //bullets
        public Texture2D basicBullet;

        //UI
        public Texture2D cursorTexture;

        //custom
        public Texture2D redTexture;
        public Texture2D blueTexture;
        public Texture2D greenTexture;

        //fonts
        public SpriteFont basicFont;

        public void LoadContent(Main game)
        {
            //tilemaps
            dungeonTileMapTexture = game.Content.Load<Texture2D>("Art/TileMaps/dungeonTiles");

            //player
            player_idle = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_idle");
            player_moveUp = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_walk_up");
            player_moveX = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_walk");
            player_radius = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_radius");

            Color[] radiusData = new Color[player_radius.Width * player_radius.Height];
            player_radius.GetData(0, null, radiusData, 0, radiusData.Length);
            for (int i = 0; i < radiusData.Length; i++)
                if (radiusData[i] != Color.Transparent) radiusData[i] = Color.White * 0.01f;
            player_radius.SetData(radiusData);

            //training dummy
            dummy_hit = game.Content.Load<Texture2D>("Art/Entities/TrainingDummy/testdummy_hit");
            dummy_idle = game.Content.Load<Texture2D>("Art/Entities/TrainingDummy/testdummy");

            //lights
            torch = game.Content.Load<Texture2D>("Art/Entities/Lights/torch");

            //containers
            chest = game.Content.Load<Texture2D>("Art/Entities/Containers/chest");
            chest_opening = game.Content.Load<Texture2D>("Art/Entities/Containers/chest_open");

            //items
            rayGun = game.Content.Load<Texture2D>("Art/Entities/Items/gun_test");

            //bullets
            basicBullet = game.Content.Load<Texture2D>("Art/Entities/Items/bullet_test");

            //UI
            cursorTexture = game.Content.Load<Texture2D>("Art/UI/cursor");

            //custom
            redTexture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blueTexture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            greenTexture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Color[] a_red = Enumerable.Range(0, 1).Select(i => Color.Red).ToArray();
            Color[] a_blue = Enumerable.Range(0, 1).Select(i => Color.Blue).ToArray();
            Color[] a_green = Enumerable.Range(0, 1).Select(i => Color.Green).ToArray();
            redTexture.SetData(a_red);
            blueTexture.SetData(a_blue);
            greenTexture.SetData(a_green);

            //Fonts
            basicFont = game.Content.Load<SpriteFont>("Fonts/Basic");
        }
    }
}
