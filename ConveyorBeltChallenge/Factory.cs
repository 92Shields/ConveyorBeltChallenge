using ConveyorBeltChallenge.Models;
using ConveyorBeltChallenge.Services;

namespace ConveyorBeltChallenge
{
    public class Factory
    {
        private readonly IComponentService _componentService;
        private readonly IConveyorBeltService _conveyorBeltService;
        private readonly IWorkerService _workerService;

        public ConveyorBelt Belt { get; set; }
        public List<Worker> Workers { get; set; }
        private int TypeACount { get; set; } = 0;
        private int TypeBCount { get; set; } = 0;
        private int TypeCCount { get; set; } = 0;

        public Factory(
            IComponentService componentService,
            IConveyorBeltService conveyorBeltService,
            IWorkerService workerService,
            int conveyorLength)
        {
            if (conveyorLength <= 0)
            {
                throw new ArgumentException("Factory conveyor length can't be 0 or less");
            }

            _componentService = componentService;
            _conveyorBeltService = conveyorBeltService;
            _workerService = workerService;

            Belt = new ConveyorBelt(conveyorLength);
            Workers = [];

            //Add two workers for every slot on the conveyor belt.
            for (var i = 1; i <= conveyorLength; i++)
            {
                Workers.Add(new Worker(i));
                Workers.Add(new Worker(i));
            }
        }

        public string Process(int steps)
        {
            if (steps <= 0)
            {
                throw new ArgumentException("Invalid number of steps provided.");
            }
            if (Belt == null || Belt.Slots == null || Belt.Slots.Length <= 0)
            {
                throw new ArgumentException("Factory Belt was invalid.");
            }
            if (Workers == null || Workers.Count != Belt.Slots.Length * 2)
            {
                throw new ArgumentException("Factory Workers were invalid.");
            }


            for (var i = 1; i <= steps; i++)
            {
                var component = _conveyorBeltService.MoveBelt(Belt, _componentService.GetRandomComponent()); //move conveyor along and get the last item from the conveyor
                switch (component.ComponentType)
                {
                    case ComponentType.TypeA:
                        TypeACount++;
                        break;
                    case ComponentType.TypeB:
                        TypeBCount++;
                        break;
                    case ComponentType.TypeC:
                        TypeCCount++;
                        break;
                    case ComponentType.Nothing:
                        break;
                    default:
                        break;
                }

                //iterate through conveyor slots
                for (var conveyorSlot = 1; conveyorSlot <= Belt.Slots.Length; conveyorSlot++)
                {
                    var slotWorkers = Workers.Where(x => x.Position == conveyorSlot).ToList();
                    if (slotWorkers.Count != 2)
                    {
                        throw new Exception("Incorrect number of workers for the conveyor position.");
                    }

                    var componentAtPosition = _conveyorBeltService.GetComponentAtPosition(Belt, conveyorSlot);
                    if (componentAtPosition != null)
                    {
                        Worker priorityWorker;
                        Worker secondaryWorker;
                        //Prioritise a worker who will fill their inventory with this component
                        if (_workerService.DoesWorkerNeedComponentUrgently(slotWorkers[1], component.ComponentType))
                        {
                            priorityWorker = slotWorkers[1];
                            secondaryWorker = slotWorkers[0];
                        }
                        else
                        {
                            priorityWorker = slotWorkers[0];
                            secondaryWorker = slotWorkers[1];
                        }

                        //If the worker has type A and B, create type C and don't interact with the conveyor belt.
                        Component? result = null;
                        var workerHasProcessedInventory = _workerService.ProcessInventory(priorityWorker);
                        if (!workerHasProcessedInventory)
                        {
                            result = _workerService.ProcessComponent(priorityWorker, componentAtPosition);
                        }

                        workerHasProcessedInventory = _workerService.ProcessInventory(secondaryWorker);
                        //first worker didn't process anything, try next worker
                        if (!workerHasProcessedInventory && result == null)
                        {
                            result = _workerService.ProcessComponent(secondaryWorker, componentAtPosition);
                        }

                        //if we have a result then it goes on the conveyor belt
                        _conveyorBeltService.PutComponentAtPosition(Belt, conveyorSlot, result ?? componentAtPosition); //if result is null it hasn't been processed so return the original component.
                    }
                }
            }

            return $"Component Type A: {TypeACount}, Component Type B: {TypeBCount}, Finished Product C: {TypeCCount}";
        }
    }
}
