using System;
using Microsoft.Xna.Framework;

namespace JustSlots
{
    public class Credit : IRenderable
    {
        private SlotMachineNumberRenderer numberRenderer;

        public JustSlotsGame Game { get; set; }
        public int Value { get; set; }
        public Vector2 Position { get; set; }

        public void Draw(GameTime gt)
        {
            Game.SpriteBatch.Begin();
            numberRenderer.DrawNumber(Value, Position);
            Game.SpriteBatch.End();
        }

        public void Initalize()
        {
            numberRenderer = Game.CreateComponent<SlotMachineNumberRenderer>();
        }
    }
}
