namespace ConveyorBeltChallenge.Models
{
    public class ConveyorBelt
    {
        public Component[] Slots { get; }
        public ConveyorBelt(int beltLength)
        {
            if (beltLength <= 0)
            {
                throw new ArgumentException("Belt Length can't be 0 or less");
            }
            Slots = new Component[beltLength];
            for (var i = 0; i < beltLength; i++)
            {
                Slots[i] = new Component(ComponentType.Nothing);
            }
        }
    }
}
