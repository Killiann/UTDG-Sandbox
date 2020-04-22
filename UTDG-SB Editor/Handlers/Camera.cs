using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB_Editor.Handlers
{
    class Camera
    {
        private Vector2 tempPos;
        private Vector2 position;
        private float zoom = 1.0f;
        private readonly float minZoom = 0.5f;
        private readonly float maxZoom = 2.0f;
        private Viewport viewport;
        public Matrix translationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)position.X, -(int)position.Y, 0)
                    * Matrix.CreateScale(zoom, zoom, 1)
                    * Matrix.CreateTranslation(new Vector3(new Vector2(viewport.Width / 2, viewport.Height / 2), 0));                
            }
        }
      
        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            tempPos = position;
        }

        public void ChangePosition(Vector2 alteredPos)
        {
            position = tempPos += alteredPos;
        }

        public void ChangeZoom(float scrollVal)
        {
            if (scrollVal < 0 && zoom > minZoom || scrollVal > 0 && zoom < maxZoom) 
                zoom += scrollVal;
        }        
    }
}
