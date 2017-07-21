using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace JustSlots
{
    public class Slot : IUpdateable, IRenderable
    {
        public const float SlotScale = 0.45f;
        public const float SlotSize = 150 * SlotScale;
        private const float SlotSpacing = 22;
        private const float FullSlotSize = SlotSize + SlotSpacing;

        private int allowTheSameFruitsCount;
        private List<int> fruits;
        private Vector2 spinPosition;
        private Vector2 spinVelocity;
        private Vector2 spinAcceleration;

        public JustSlotsGame Game { get; set; }
        public Vector2 Position { get; set; }
        public bool IsSpinning { get; private set; }

        public Slot()
        {
            fruits = new List<int>() { 0, 0, 0, 0 };
        }

        public int GetSlot(int row)
        {
            return fruits[row];
        }

        public void Draw(GameTime gt)
        {
            Game.SpriteBatch.Begin();

            for (int i = 0; i < fruits.Count; i++)
            {
                Game.SpriteBatch.Draw(Game.Textures.Fruits, Position + new Vector2(0, FullSlotSize) * i + spinPosition - new Vector2(0, SlotSize), GetFruit(fruits[i]), Color.White, 0f, Vector2.Zero, SlotScale, SpriteEffects.None, 0f);
            }

            Game.SpriteBatch.End();
        }

        public void Spin(float force)
        {
            IsSpinning = true;
            spinAcceleration = new Vector2(0, force);
            allowTheSameFruitsCount = JustSlotsGame.Random.Next(0, 3);
        }

        public void Initalize()
        {

        }

        public void Update(GameTime gt)
        {
            spinPosition += spinVelocity;
            spinVelocity += spinAcceleration;
            spinVelocity *= 0.99f;

            if (spinVelocity.Length() < 0.1f && spinPosition.Y != 0)
            {
                spinVelocity = Vector2.Zero;

                if (spinPosition.Y > 0 && spinPosition.Y < FullSlotSize / 2f)
                    spinPosition.Y -= spinPosition.Y / FullSlotSize * 2f + (Math.Abs(spinPosition.Y) > 2 ? 1f : 0f);
                else if (spinPosition.Y <= FullSlotSize && spinPosition.Y > FullSlotSize / 2f)
                    spinPosition.Y += spinPosition.Y / FullSlotSize * 2f + (Math.Abs(spinPosition.Y) > 2 ? 1f : 0f);

                if (Math.Abs(spinPosition.Y) <= 0.5f)
                {
                    Game.SoundManager.SlotSound1.Play(1, 0f, 0f);
                    spinPosition.Y = 0;
                    IsSpinning = false;
                }
            }

            if (spinPosition.Y > FullSlotSize)
            {
                spinPosition.Y -= ((int)spinPosition.Y / (int)FullSlotSize) * FullSlotSize;

                int fruit;
                Game.SoundManager.SlotSound1.Play(1, MathHelper.Clamp(spinVelocity.Length() / 100f, 0, 1), 0f);

                do
                {
                    fruit = JustSlotsGame.Random.Next(0, 3) == 0 ? JustSlotsGame.Random.Next(0, 8) : JustSlotsGame.Random.Next(0, 7);
                }
                while (fruits.Count(p => p == fruit) > allowTheSameFruitsCount);

                fruits.Insert(0, fruit);
                fruits.RemoveAt(fruits.Count - 1);
            }
          

            spinAcceleration = Vector2.Zero;
        }

        private Rectangle GetFruit(int id)
        {
            return new Rectangle(id * 150, 0, 150, 150);
        }
    }
}
