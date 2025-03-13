using ConveyorBeltChallenge.Models;

namespace ConveyorBeltChallenge.Services
{
    public class ComponentService : IComponentService
    {
        public Component GetRandomComponent()
        {
            var randomInt = new Random().Next(1, 4);

            return randomInt switch
            {
                1 => new Component(ComponentType.TypeA),
                2 => new Component(ComponentType.TypeB),
                3 => new Component(ComponentType.Nothing),
                _ => new Component(ComponentType.Nothing),
            };
        }
    }
}
