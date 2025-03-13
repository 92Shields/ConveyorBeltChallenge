using ConveyorBeltChallenge.Models;

namespace ConveyorBeltChallenge.Services
{
    public interface IWorkerService
    {
        bool ProcessInventory(Worker worker);
        Component? ProcessComponent(Worker worker, Component component);
        bool DoesWorkerNeedComponentUrgently(Worker worker, ComponentType componentType);
    }
}
