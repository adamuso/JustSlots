using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace JustSlots
{
    public class SlotsMachine : IUpdateable, IRenderable
    {
        private Slot[] slots;
        private MouseState lastMouseSate;
        private Credit credit;
        private bool isSpinning;
        private Payout payout;

        public JustSlotsGame Game { get; set; }

        public void Draw(GameTime gt)
        {
            Viewport old = Game.GraphicsDevice.Viewport;

            Game.GraphicsDevice.Viewport = new Viewport(new Rectangle(
                old.Width / 2 - Game.Textures.SlotsMachine.Width / 2,
                old.Height / 2 - Game.Textures.SlotsMachine.Height / 2,
                Game.Textures.SlotsMachine.Width,
                Game.Textures.SlotsMachine.Height));

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(Game.Textures.BlankTexture, new Rectangle(0, 0, Game.Textures.SlotsMachine.Width, Game.Textures.SlotsMachine.Height), new Color(60, 80, 110));
            Game.SpriteBatch.End();

            foreach (Slot slot in slots)
            {
                slot.Draw(gt);
            }

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(Game.Textures.SlotsMachine, Vector2.Zero, Color.White);
            Game.SpriteBatch.End();

            credit.Draw(gt);
            payout.Draw(gt);

            Game.GraphicsDevice.Viewport = old;
        }

        public void Initalize()
        {
            slots = new Slot[3];
            slots[0] = Game.CreateComponent<Slot>();
            slots[1] = Game.CreateComponent<Slot>();
            slots[2] = Game.CreateComponent<Slot>();

            slots[0].Position = new Vector2(126 + (180 - 24) / 2 - Slot.SlotSize / 2, 69);
            slots[1].Position = new Vector2(306 + (180 - 24) / 2 - Slot.SlotSize / 2, 69);
            slots[2].Position = new Vector2(486 + (180 - 24) / 2 - Slot.SlotSize / 2, 69);

            credit = Game.CreateComponent<Credit>();
            credit.Position = new Vector2(219, 504);
            credit.Value = 500;

            payout = Game.CreateComponent<Payout>();
            payout.Position = new Vector2(436, 504);
        }

        public void Spin()
        {
            foreach (Slot slot in slots)
            {
                float force = JustSlotsGame.Random.Next(100, 150);

                slot.Spin(force);
            }

            isSpinning = true;
        }

        public void FindLines()
        {
            //bool doubleUsed = false;

            for(int y = 0; y < 3; y++)
            {
                bool isLine = true;
                bool isDouble = false;
                int doubleVal = slots[0].GetSlot(y + 1);
                int val = slots[0].GetSlot(y + 1);

                for (int x = 1; x < 3; x++)
                {
                    if (slots[x].GetSlot(y + 1) == doubleVal)
                        isDouble = true;

                    if (slots[x].GetSlot(y + 1) != val)
                        isLine = false;

                    if(!isDouble)
                        doubleVal = slots[x].GetSlot(y + 1);
                }

                if (isLine)
                    payout.Value += val == 7 ? 700 : 100;
                else
                {
                    if (isDouble) //&& !doubleUsed)
                    {
                        //doubleUsed = true;
                        payout.Value += doubleVal == 7 ? 70 : 20;
                    }
                }
            }
        }

        public void Update(GameTime gt)
        {
            bool areSpinning = false;

            foreach (Slot slot in slots)
            {
                slot.Update(gt);

                if (isSpinning && slot.IsSpinning)
                    areSpinning = true;
            }

            if (isSpinning && !areSpinning)
            {
                isSpinning = false;

                FindLines();

                if(payout.Value != 0)
                    payout.Show();
            }

            if (payout.Value != 0 && payout.IsHidden)
                AddPayoutToCredit();

            if (Mouse.GetState().RightButton == ButtonState.Pressed && lastMouseSate.RightButton == ButtonState.Released && credit.Value >= 50 && !isSpinning) 
            {
                AddPayoutToCredit();
                Spin();
                credit.Value -= 50;
            }

            lastMouseSate = Mouse.GetState();
        }

        private void AddPayoutToCredit()
        {
            if (payout.Value <= 0)
                return;

            int value = payout.Value;
            payout.Hide();
            payout.Value = 0;

            credit.Value += value;
        }
    }
}
