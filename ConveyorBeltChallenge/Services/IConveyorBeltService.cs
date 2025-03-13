using ConveyorBeltChallenge.Models;

namespace ConveyorBeltChallenge.Services
{
    public interface IConveyorBeltService
    {
        Component MoveBelt(ConveyorBelt conveyor, Component newComponent);
        Component GetComponentAtPosition(ConveyorBelt conveyor, int position);
        void PutComponentAtPosition(ConveyorBelt conveyor, int position, Component component);
    }
}
