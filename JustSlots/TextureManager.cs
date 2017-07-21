using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustSlots
{
    public class TextureManager : IGameComponent
    {
        public JustSlotsGame Game { get; set; }

        public void LoadContent()
        {
            Fruits = Game.Content.Load<Texture2D>("fruits");
            SlotsMachine = Game.Content.Load<Texture2D>("slotsmachine");
            Numbers = Game.Content.Load<Texture2D>("numbers");

            BlankTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new Color[] { Color.White });
        }

        public void Initalize()
        {

        }

        public Texture2D Fruits { get; private set; }
        public Texture2D BlankTexture { get; private set; }
        public Texture2D SlotsMachine { get; private set; }
        public Texture2D Numbers { get; private set; }
    }
}
