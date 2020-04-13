using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB.Map
{
    class Camera
    {
        private Vector2 position;
        private float scale = 1.1f;
        private int viewportWidth;
        private int viewportHeight;
        public Vector2 VPCenter { get { return new Vector2(viewportWidth * 0.5f, viewportHeight * 0.5f); } }
        
        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)position.X, -(int)position.Y, 0)
                    * Matrix.CreateScale(scale, scale, 1)
                    * Matrix.CreateTranslation(new Vector3(VPCenter, 0));
            }            
        }

        public Camera(Viewport viewport)
        {
            viewportWidth = viewport.Width;
            viewportHeight = viewport.Height;            
        }

        public void ResetViewPort(Viewport viewport)
        {
            viewportWidth = viewport.Width;
            viewportHeight = viewport.Height;
            Console.WriteLine(viewportWidth);
            scale = (float)viewportWidth / 1000 * 1.4f;
        }

        public Vector2 WorldToScreen(Vector2 coord)
        {
            return Vector2.Transform(coord, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 coord)
        {
            return Vector2.Transform(coord, Matrix.Invert(TranslationMatrix));
        }

        public void SetPosition(Vector2 newPos)
        {
            position = newPos;
        }
    }
}
