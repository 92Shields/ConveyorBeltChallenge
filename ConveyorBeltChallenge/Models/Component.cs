namespace ConveyorBeltChallenge.Models
{
    public class Component(ComponentType componentType)
    {
        public ComponentType ComponentType { get; } = componentType;
    }
}
