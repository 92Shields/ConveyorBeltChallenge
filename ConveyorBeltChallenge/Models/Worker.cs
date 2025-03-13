namespace ConveyorBeltChallenge.Models
{
    public class Worker
    {
        public int Position { get; }
        public bool HasTypeAComponent { get; set; } = false;
        public bool HasTypeBComponent { get; set; } = false;
        public bool HasTypeCComponent { get; set; } = false;

        public Worker(int conveyorPosition)
        {
            if (conveyorPosition <= 0)
            {
                throw new ArgumentException("Worker conveyor position cannot be less than 1");
            }

            Position = conveyorPosition;
        }
    }
}
