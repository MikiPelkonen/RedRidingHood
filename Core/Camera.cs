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
                            MathHelper.Clamp(-target.Position.X - (target.Rectangle.Width / 2), -320, -320),
                            MathHelper.Clamp(-target.Position.Y - (target.Rectangle.Height / 2), -525, -180),
                            0);

            Transform = position * offset;
        }
    }
}
