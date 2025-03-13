using ConveyorBeltChallenge.Models;

namespace ConveyorBeltChallenge.Services
{
    public class WorkerService : IWorkerService
    {

        /// <summary>
        /// Determines whether the worker will process the items in it's inventory.
        /// Returns true if it did and false if not.
        /// </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool ProcessInventory(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentException("Worker canot be null");
            }

            if (worker.HasTypeAComponent && worker.HasTypeBComponent)
            {
                //worker has both component, process them and create type c component
                worker.HasTypeAComponent = false;
                worker.HasTypeBComponent = false;
                worker.HasTypeCComponent = true;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Takes a worker and a component as params.
        /// Returns null if the worker did nothing.
        /// Returns Component with type nothing if it took the component.
        /// Returns Component with type C if it is placing it back.
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Component? ProcessComponent(Worker worker, Component component)
        {
            if (worker == null || component == null)
            {
                throw new ArgumentException("Process component invalid params");
            }

            if (component.ComponentType == ComponentType.TypeC)
            {
                //this has been completed, don't process it.
                return null;
            }

            if (worker.HasTypeCComponent && component.ComponentType == ComponentType.Nothing)
            {
                //worker has a completed product, put it on the conveyor and empty their components if the conveyor is empty
                worker.HasTypeAComponent = false;
                worker.HasTypeBComponent = false;
                worker.HasTypeCComponent = false;
                return new Component(ComponentType.TypeC);
            }

            if (!worker.HasTypeCComponent && component.ComponentType == ComponentType.TypeA && !worker.HasTypeAComponent)
            {
                //if we have no type A components and the conveyor has a type A, pick it up
                worker.HasTypeAComponent = true;
                return new Component(ComponentType.Nothing);
            }

            if (!worker.HasTypeCComponent && component.ComponentType == ComponentType.TypeB && !worker.HasTypeBComponent)
            {
                //if we have no type B components and the conveyor has a type B, pick it up
                worker.HasTypeBComponent = true;
                return new Component(ComponentType.Nothing);
            }

            return null;
        }

        /// <summary>
        /// Returns true if the worker has one component and needs the other.
        /// Returns false otherwise.
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public bool DoesWorkerNeedComponentUrgently(Worker worker, ComponentType componentType)
        {
            if (worker == null || componentType == ComponentType.TypeC)
            {
                return false; //If worker has type C, it doesn't need to pick up a component
            }

            if (componentType == ComponentType.TypeA && worker.HasTypeBComponent && !worker.HasTypeAComponent)
            {
                return true; //If worker has type B and not type A, it needs type A
            }

            if (componentType == ComponentType.TypeB && worker.HasTypeAComponent && !worker.HasTypeBComponent)
            {
                return true; //If worker has type A and not type B, it needs type B
            }

            return false;
        }
    }
}
