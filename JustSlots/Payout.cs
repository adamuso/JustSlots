using Microsoft.Xna.Framework;

namespace JustSlots
{
    public class Payout : IRenderable
    {
        private SlotMachineNumberRenderer numberRenderer;
        private int showTime;

        public JustSlotsGame Game { get; set; }
        public int Value { get; set; }
        public Vector2 Position { get; set; }
        public bool IsHidden { get { return showTime <= 0; } }

        public void Show()
        {
            showTime = 4000;
        }

        public void Hide()
        {
            showTime = 0;
        }
        
        public void Draw(GameTime gt)
        {
            if (showTime > 0)
            {
                if (showTime % 400 > 200)
                {
                    Game.SpriteBatch.Begin();
                    numberRenderer.DrawNumber(Value, Position);
                    Game.SpriteBatch.End();
                }

                showTime -= gt.ElapsedGameTime.Milliseconds;
            }
        }

        public void Initalize()
        {
            numberRenderer = Game.CreateComponent<SlotMachineNumberRenderer>();
        }
    }
}
