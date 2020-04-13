using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_SB.Entities;

namespace UTDG_SB.Handlers
{
    public class PhysicsHandler
    {
        public bool isMoving = false;
        private Vector2 acceleration;

        public float accelerationValue = 0.2f;
        public float frictionValue = 0.5f;
        public float maxVelocity = 3.0f;

        public void SetYAcceleration(float newVel) { acceleration.Y = newVel; }
        public void SetXAcceleration(float newVel) { acceleration.X = newVel; }

        public void Update(Player player)
        {
            if (isMoving)
            {
                //x
                if (acceleration.X != 0)
                {
                    if (player.GetVelocity().X <= maxVelocity && player.GetVelocity().X >= -maxVelocity)
                        player.SetXVelocity(player.GetVelocity().X + acceleration.X);
                    else
                    {
                        if (player.GetVelocity().X < -maxVelocity)
                            player.SetXVelocity(-maxVelocity);
                        else player.SetXVelocity(maxVelocity);
                    }
                }

                //y
                if (acceleration.Y != 0)
                {
                    if (player.GetVelocity().Y <= maxVelocity && player.GetVelocity().Y >= -maxVelocity)
                        player.SetYVelocity(player.GetVelocity().Y + acceleration.Y);
                    else
                    {
                        if (player.GetVelocity().Y < -maxVelocity)
                            player.SetYVelocity(-maxVelocity);
                        else player.SetYVelocity(maxVelocity);
                    }
                }
            }

            if (acceleration.X == 0)
            {
                if (player.GetVelocity().X > 0)
                {
                    if (player.GetVelocity().X > 0.25) player.SetXVelocity(player.GetVelocity().X * frictionValue);
                    else player.SetXVelocity(0);
                }
                if (player.GetVelocity().X < 0)
                {
                    if (player.GetVelocity().X < -0.25) player.SetXVelocity(player.GetVelocity().X * frictionValue);
                    else player.SetXVelocity(0);
                }
            }
            if(acceleration.Y == 0) { 
                if (player.GetVelocity().Y > 0)
                {
                    if (player.GetVelocity().Y > 0.25) player.SetYVelocity(player.GetVelocity().Y * frictionValue);
                    else player.SetYVelocity(0);
                }
                if (player.GetVelocity().Y < 0)
                {
                    if (player.GetVelocity().Y < -0.25) player.SetYVelocity(player.GetVelocity().Y * frictionValue);
                    else player.SetYVelocity(0);
                }
            }

            acceleration = new Vector2(0, 0);
        }
    }
}
