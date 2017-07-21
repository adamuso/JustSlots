using System;
using Microsoft.Xna.Framework;

namespace JustSlots
{
    public class SlotMachineNumberRenderer : IGameComponent
    {
        public JustSlotsGame Game { get; set; }

        public void DrawNumber(int number, Vector2 position)
        {
            if (number > 99999 || number < 0)
                throw new InvalidOperationException();

            string numText = number.ToString();

            for (int i = 0; i < numText.Length; i++)
            {
                Game.SpriteBatch.Draw(Game.Textures.Numbers, position + new Vector2((6 + 18) * (5 - numText.Length) + i * (6 + 18), 0), new Rectangle((numText[i] - '0') * 18, 0, 18, 21), Color.White);
            }

            for(int i = 0; i < 5 - numText.Length; i++)
            {
                Game.SpriteBatch.Draw(Game.Textures.Numbers, position + new Vector2(i * (6 + 18), 0), new Rectangle(0, 0, 18, 21), Color.White);

            }
        }

        public void Initalize()
        {

        }
    }
}
