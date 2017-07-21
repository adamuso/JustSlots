using Microsoft.Xna.Framework.Audio;
using System;

namespace JustSlots
{
    public class SoundManager : IGameComponent
    {
        public JustSlotsGame Game { get; set; }

        public void LoadContent()
        {
            SlotSound1 = Game.Content.Load<SoundEffect>("slotsound1");
        }

        public void Initalize()
        {

        }

        public SoundEffect SlotSound1 { get; private set; }
    }
}