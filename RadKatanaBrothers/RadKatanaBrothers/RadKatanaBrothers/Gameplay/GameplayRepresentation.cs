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
        public static event Created onCreated;
        public static event Terminated onTerminated;
        public override void Create()
        {
            if (onCreated != null)
                onCreated(this);
        }
        public override void Terminate()
        {
            if (onTerminated != null)
                onTerminated(this);
        }
        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
                Parent.GetRepresentation<PhysicsRepresentation>("physics").ApplyForce(new Vector2(mouse.X, mouse.Y) - Parent.AddProperty<Vector2>("position", new Vector2(300, 250)).Value);
        }
    }
}
