using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RadKatanaBrothers
{
    class GameplayRepresentation : Representation
    {
        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
                Parent.GetRepresentation<PhysicsRepresentation>("Physics").ApplyForce(new Vector2(mouse.X, mouse.Y) - Parent.AddProperty<Vector2>("position", new Vector2(300, 250)).Value);
        }
    }
}
