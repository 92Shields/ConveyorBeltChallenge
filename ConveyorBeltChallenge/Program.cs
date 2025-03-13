using ConveyorBeltChallenge;
using ConveyorBeltChallenge.Services;

try
{
    while (true)
    {
        Console.WriteLine("Please enter a number for the length of the conveyor belt");
        if (!int.TryParse(Console.ReadLine(), out var conveyorLength))
        {
            Console.WriteLine("Conveyor length must be an integer.");
            Console.Read();
            Environment.Exit(0);
        }

        Console.WriteLine("Please enter a number for the number of iterations");
        if (!int.TryParse(Console.ReadLine(), out var stepCount))
        {
            Console.WriteLine("Iterations must be an integer.");
            Console.Read();
            Environment.Exit(0);
        }


        var factory = new Factory(new ComponentService(), new ConveyorBeltService(), new WorkerService(), conveyorLength);
        var result = factory.Process(stepCount);

        Console.WriteLine(result);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}