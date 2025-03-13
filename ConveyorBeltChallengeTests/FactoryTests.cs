using ConveyorBeltChallenge;
using ConveyorBeltChallenge.Models;
using ConveyorBeltChallenge.Services;
using Moq;

namespace ConveyorBeltChallengeTests
{
    public class FactoryTests
    {
        private readonly Mock<IComponentService> _componentService = new();
        private readonly Mock<IConveyorBeltService> _conveyorBeltService = new();
        private readonly Mock<IWorkerService> _workerService = new();

        [Fact]
        public void Setup_whenConveyorLengthInvalid_throwsException()
        {
            try
            {
                var factory = new Factory(_componentService.Object, _conveyorBeltService.Object, _workerService.Object, 0);
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void Setup_whenOkay_setupOkay()
        {
            var factory = new Factory(_componentService.Object, _conveyorBeltService.Object, _workerService.Object, 3);

            Assert.NotNull(factory.Workers);
            Assert.NotNull(factory.Belt);

            Assert.Equal(3, factory.Belt.Slots.Length);
            Assert.Equal(6, factory.Workers.Count);

            Assert.False(factory.Belt.Slots.Where(x => x == null).Any());
            Assert.False(factory.Workers.Where(x => x == null).Any());

            Assert.Equal(3, factory.Belt.Slots.Where(x => x.ComponentType == ComponentType.Nothing).Count());
        }

        [Fact]
        public void Process_FavoursWorker()
        {
            var factory = new Factory(_componentService.Object, new ConveyorBeltService(), new WorkerService(), 2);

            _componentService.SetupSequence(x => x.GetRandomComponent())
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB));

            factory.Process(2);

            Assert.True(factory.Workers[0].HasTypeAComponent);
            Assert.True(factory.Workers[0].HasTypeBComponent);
            Assert.False(factory.Workers[0].HasTypeCComponent);
            Assert.False(factory.Workers[1].HasTypeAComponent);
            Assert.False(factory.Workers[1].HasTypeBComponent);
            Assert.False(factory.Workers[1].HasTypeCComponent);
        }

        [Fact]
        public void Process_whileOtherWorkerProcesses_workerPicksUp()
        {
            var factory = new Factory(_componentService.Object, new ConveyorBeltService(), new WorkerService(), 2);

            _componentService.SetupSequence(x => x.GetRandomComponent())
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA));

            factory.Process(3);

            Assert.False(factory.Workers[0].HasTypeAComponent);
            Assert.False(factory.Workers[0].HasTypeBComponent);
            Assert.True(factory.Workers[0].HasTypeCComponent);
            Assert.True(factory.Workers[1].HasTypeAComponent);
            Assert.False(factory.Workers[1].HasTypeBComponent);
            Assert.False(factory.Workers[1].HasTypeCComponent);
        }

        [Fact]
        public void Process_whenRuns_correctresult()
        {
            var factory = new Factory(_componentService.Object, new ConveyorBeltService(), new WorkerService(), 2);

            _componentService.SetupSequence(x => x.GetRandomComponent())
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.TypeB))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.TypeA))
                .Returns(new Component(ComponentType.Nothing))
                .Returns(new Component(ComponentType.Nothing));


            var result = factory.Process(30);

            Assert.Equal("Component Type A: 0, Component Type B: 3, Finished Product C: 7", result);
            Assert.Equal(ComponentType.Nothing, factory.Belt.Slots[0].ComponentType);
            Assert.Equal(ComponentType.TypeC, factory.Belt.Slots[1].ComponentType);

            Assert.True(factory.Workers[0].HasTypeAComponent);
            Assert.False(factory.Workers[0].HasTypeBComponent);
            Assert.False(factory.Workers[0].HasTypeCComponent);
            Assert.False(factory.Workers[1].HasTypeAComponent);
            Assert.False(factory.Workers[1].HasTypeBComponent);
            Assert.False(factory.Workers[1].HasTypeCComponent);
            Assert.False(factory.Workers[2].HasTypeAComponent);
            Assert.False(factory.Workers[2].HasTypeBComponent);
            Assert.False(factory.Workers[2].HasTypeCComponent);
            Assert.False(factory.Workers[3].HasTypeAComponent);
            Assert.False(factory.Workers[3].HasTypeBComponent);
            Assert.True(factory.Workers[3].HasTypeCComponent);
        }
    }
}
