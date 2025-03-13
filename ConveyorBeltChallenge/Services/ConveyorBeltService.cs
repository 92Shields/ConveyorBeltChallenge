using ConveyorBeltChallenge.Models;

namespace ConveyorBeltChallenge.Services
{
    public class ConveyorBeltService : IConveyorBeltService
    {
        /// <summary>
        /// Returns the component in the last position of the belt.
        /// Puts a new random component at the front of the belt.
        /// Shifts the existing components one place down.
        /// </summary>
        /// <param name="conveyor"></param>
        /// <param name="newComponent"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Component MoveBelt(ConveyorBelt conveyor, Component newComponent)
        {
            if (conveyor == null || conveyor.Slots == null || conveyor.Slots.Length <= 0 || newComponent == null)
            {
                throw new ArgumentException("MoveBelt received incorrect parameter");
            }

            var last = conveyor.Slots[^1]; //index operator for last index

            //Shift all items 1 place down the conveyor
            for (int i = conveyor.Slots.Length - 1; i > 0; i--)
            {
                conveyor.Slots[i] = conveyor.Slots[i - 1];
            }

            //Add the new component to the conveyor;
            conveyor.Slots[0] = newComponent;

            //return the item removed from the end of the conveyor
            return last;
        }

        /// <summary>
        /// Returns the component at the given position.
        /// Position is not an index and begins at 1.
        /// </summary>
        /// <param name="conveyor"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Component GetComponentAtPosition(ConveyorBelt conveyor, int position)
        {
            if (conveyor == null || conveyor.Slots == null || conveyor.Slots.Length < 1 || conveyor.Slots.Length < position || position <= 0)
            {
                throw new ArgumentException("GetComponentAtPosition received an invalid parameter.");
            }

            return conveyor.Slots[position - 1];
        }

        /// <summary>
        /// Puts a component into the given position replacing the component that was there.
        /// Position is not an index and begins at 1.
        /// </summary>
        /// <param name="conveyor"></param>
        /// <param name="position"></param>
        /// <param name="component"></param>
        /// <exception cref="ArgumentException"></exception>
        public void PutComponentAtPosition(ConveyorBelt conveyor, int position, Component component)
        {
            if (conveyor == null || conveyor.Slots == null || conveyor.Slots.Length < 1 || conveyor.Slots.Length < position || position <= 0 || component == null)
            {
                throw new ArgumentException("PutComponentAtPosition received an invalid parameter.");
            }

            conveyor.Slots[position - 1] = component;
        }
    }
}
