using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Character target)
        {
            var offset = Matrix.CreateTranslation(
                                RedRidingHoodGame.ScreenWidth / 4,
                                RedRidingHoodGame.ScreenHeight / 4,
                                0);

            var position = Matrix.CreateTranslation(
                            -target.Position.X - (target.Rectangle.Width / 2),
                            -target.Position.Y - (target.Rectangle.Height / 2),
                            0);

            Transform = position * offset;
        }
    }
}
