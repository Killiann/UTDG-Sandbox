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

        //training dummy
        public Texture2D dummy_idle;
        public Texture2D dummy_hit;

        //custom
        public Texture2D redTexture;
        public Texture2D blueTexture;
        public Texture2D greenTexture;

        public void LoadContent(Main game)
        {
            //tilemaps
            dungeonTileMapTexture = game.Content.Load<Texture2D>("Art/TileMaps/dungeonTiles");

            //player
            player_idle = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_idle");
            player_moveUp = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_walk_up");
            player_moveX = game.Content.Load<Texture2D>("Art/DynamicEntities/Player/player_walk");

            //training dummy
            dummy_hit = game.Content.Load<Texture2D>("Art/Entities/TrainingDummy/testdummy_hit");
            dummy_idle = game.Content.Load<Texture2D>("Art/Entities/TrainingDummy/testdummy");

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
        }
    }
}
