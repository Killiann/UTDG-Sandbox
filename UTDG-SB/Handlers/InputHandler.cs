using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_SB.Entities;

namespace UTDG_SB.Handlers
{
    class InputHandler
    {
        public void Update(PhysicsHandler physicsHandler)
        {
            physicsHandler.isMoving = false;
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.W))
            {
                physicsHandler.isMoving = true;
                physicsHandler.SetYAcceleration(-physicsHandler.accelerationValue);
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                physicsHandler.isMoving = true;
                physicsHandler.SetXAcceleration(-physicsHandler.accelerationValue);
            }
            if (kbState.IsKeyDown(Keys.S))
            {
                physicsHandler.isMoving = true;
                physicsHandler.SetYAcceleration(physicsHandler.accelerationValue);
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                physicsHandler.isMoving = true;
                physicsHandler.SetXAcceleration(physicsHandler.accelerationValue);
            }
        }
    }
}
