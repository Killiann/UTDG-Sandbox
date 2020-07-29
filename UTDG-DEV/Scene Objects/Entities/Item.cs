using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Handlers;

namespace UTDG_DEV.Scene_Objects.Entities
{
    public class Item
    {
        protected Texture2D texture;
        protected int itemID;
        protected Scene scene;

        public Texture2D getTexture() { return texture; }

        public Item(Scene scene, int itemID, Texture2D texture)
        {
            this.scene = scene;
            this.texture = texture;
            this.itemID = itemID;
        }
    }

    public class Weapon : Item
    {
        protected Texture2D heldWeaponTexture;
        protected int damage;
        public Weapon(Scene scene, int itemID, int damage, Texture2D texture, Texture2D heldWeaponTexture) : base(scene, itemID, texture)
        {
            this.heldWeaponTexture = heldWeaponTexture;
            this.damage = damage;
        }
    }

    public class RangedWeapon : Weapon
    {
        private readonly int fireRate;
        private int fireTimer = 0;
        private readonly int bulletSize;
        private readonly int bulletSpeed;
        private readonly Texture2D bulletTexture;
        private Vector2 Position() { return scene.player.GetPosition(); }
        private bool isFiring = false;
        private float rotation = 0f;
        private Vector2 bulletOffset = new Vector2(8, 4);
        private float depth = 0.55f;
        public RangedWeapon(Scene scene, int itemID, int damage, int fireRate, int bulletSize, int bulletSpeed, Texture2D texture, Texture2D heldWeaponTexture, Texture2D bulletTexture) : base(scene, itemID, damage, texture, heldWeaponTexture)
        {
            this.fireRate = fireRate;
            this.bulletSize = bulletSize;
            this.bulletSpeed = bulletSpeed;
            this.bulletTexture = bulletTexture;            
        }

        public void Fire(){ isFiring = true; }

        public void Update(Vector2 targetPos)
        {
            rotation = AngleBetween(Position(), targetPos);
            depth = targetPos.Y < Position().Y-32 ? 0.45f : 0.55f;

            if (isFiring)
            {
                if (fireTimer < fireRate)
                    fireTimer++;
                else
                {
                    fireTimer = 0;
                    Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                    Vector2 BulletPosition = Position() + bulletOffset + (direction * 20);
                    Bullet bullet = new Bullet(scene, BulletPosition, direction * bulletSpeed, bulletTexture);
                    scene.bullets.Add(bullet);
                }
            }
            
            isFiring = false;
        }

        private float AngleBetween(Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(heldWeaponTexture, new Rectangle((int)Position().X + (int)bulletOffset.X, (int)Position().Y + (int)bulletOffset.Y, 32, 32), null, Color.White, rotation, new Vector2(5,8), rotation > -(Math.PI / 2) && rotation < (Math.PI / 2) ? SpriteEffects.None : SpriteEffects.FlipVertically, depth);
        }
    }
}
