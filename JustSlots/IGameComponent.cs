namespace JustSlots
{
    public interface IGameComponent
    {
        JustSlotsGame Game { get; set; }

        void Initalize();        
    }
}
