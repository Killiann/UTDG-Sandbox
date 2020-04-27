using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Scene_Objects.Entities;

namespace UTDG_DEV.Handlers
{
    class PhysicsHandler
    {
        private float frictionVal = 0.7f;
        private float frictionResetVal = 0.1f;
        private DynamicEntity currentEntity;

        public void Update(DynamicEntity entity)
        {
            currentEntity = entity;

            entity.velocity.X = HandleAcceleration(entity.directionVector.X, entity.velocity.X);
            entity.velocity.Y = HandleAcceleration(entity.directionVector.Y, entity.velocity.Y);            
        }
        private float HandleAcceleration(float directionVector, float velocity)
        {
            if (directionVector == 1)
            {
                if (velocity < currentEntity.GetMaxVelocity())
                    velocity += currentEntity.GetAcceleration();
                else velocity = currentEntity.GetMaxVelocity();
            }
            else if (directionVector == -1)
            {
                if (velocity > -currentEntity.GetMaxVelocity())
                    velocity -= currentEntity.GetAcceleration();
                else velocity = -currentEntity.GetMaxVelocity();
            }
            else
            {
                if (velocity != 0)
                    if (velocity < frictionResetVal && velocity > frictionResetVal)
                        velocity = 0;
                    else velocity *= frictionVal;
            }
            return velocity;
        }
    }
}
