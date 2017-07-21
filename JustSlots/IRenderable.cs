using Microsoft.Xna.Framework;

namespace JustSlots
{
    public interface IRenderable : IGameComponent
    {
        void Draw(GameTime gt);
    }
}