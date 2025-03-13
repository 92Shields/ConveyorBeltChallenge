using ConveyorBeltChallenge.Models;
using ConveyorBeltChallenge.Services;

namespace ConveyorBeltChallengeTests
{
    public class WorkerServiceTests
    {
        private readonly IWorkerService _service = new WorkerService();

        [Fact]
        public void ProcessComponent_whenWorkerNull_throwsException()
        {
            try
            {
                var result = _service.ProcessComponent(null, new Component(ComponentType.TypeA));
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }
            Assert.True(false);
        }

        [Fact]
        public void ProcessComponent_whenComponentNull_throwsException()
        {
            try
            {
                var result = _service.ProcessComponent(new Worker(1), null);
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }
            Assert.True(false);
        }

        [Fact]
        public void ProcessComponent_whenComponentComplete_doesNothing()
        {
            var worker = new Worker(1);
            var component = new Component(ComponentType.TypeC);

            var result = _service.ProcessComponent(worker, component);

            Assert.Null(result);
        }

        [Fact]
        public void ProcessComponent_whenHasTypeCAndBeltEmpty_returnsTypeCAndEmptiesInventory()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = false,
                HasTypeBComponent = false,
                HasTypeCComponent = true
            };

            var component = new Component(ComponentType.Nothing);

            var result = _service.ProcessComponent(worker, component);

            Assert.NotNull(result);
            Assert.Equal(ComponentType.TypeC, result.ComponentType);
            Assert.False(worker.HasTypeAComponent);
            Assert.False(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessComponent_whenHasNothingAndBeltEmpty_doesNothing()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = false,
                HasTypeBComponent = false,
                HasTypeCComponent = false
            };

            var component = new Component(ComponentType.Nothing);

            var result = _service.ProcessComponent(worker, component);

            Assert.Null(result);
            Assert.False(worker.HasTypeAComponent);
            Assert.False(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessComponent_whenHasNothing_picksUpTypeA()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = false,
                HasTypeBComponent = false,
                HasTypeCComponent = false
            };

            var component = new Component(ComponentType.TypeA);

            var result = _service.ProcessComponent(worker, component);

            Assert.NotNull(result);
            Assert.Equal(ComponentType.Nothing, result.ComponentType);
            Assert.True(worker.HasTypeAComponent);
            Assert.False(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessComponent_whenHasTypeB_picksUpTypeA()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = false,
                HasTypeBComponent = true,
                HasTypeCComponent = false
            };

            var component = new Component(ComponentType.TypeA);

            var result = _service.ProcessComponent(worker, component);

            Assert.NotNull(result);
            Assert.Equal(ComponentType.Nothing, result.ComponentType);
            Assert.True(worker.HasTypeAComponent);
            Assert.True(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessComponent_whenHasNothing_picksUpTypeB()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = false,
                HasTypeBComponent = false,
                HasTypeCComponent = false
            };

            var component = new Component(ComponentType.TypeB);

            var result = _service.ProcessComponent(worker, component);

            Assert.NotNull(result);
            Assert.Equal(ComponentType.Nothing, result.ComponentType);
            Assert.False(worker.HasTypeAComponent);
            Assert.True(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessComponent_whenHasTypeA_picksUpTypeB()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = true,
                HasTypeBComponent = false,
                HasTypeCComponent = false
            };

            var component = new Component(ComponentType.TypeB);

            var result = _service.ProcessComponent(worker, component);

            Assert.NotNull(result);
            Assert.Equal(ComponentType.Nothing, result.ComponentType);
            Assert.True(worker.HasTypeAComponent);
            Assert.True(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessComponent_whenHasTypeC_doesNotPicksUpType()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = false,
                HasTypeBComponent = false,
                HasTypeCComponent = true
            };

            var component = new Component(ComponentType.TypeB);

            var result = _service.ProcessComponent(worker, component);

            Assert.Null(result);
            Assert.False(worker.HasTypeAComponent);
            Assert.False(worker.HasTypeBComponent);
            Assert.True(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessInventory_throwException_whenWorkerNull()
        {
            try
            {
                var result = _service.ProcessInventory(null);
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }
            Assert.True(false);
        }

        [Fact]
        public void ProcessInventory_whenHasTypeAAndB_Processes()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = true,
                HasTypeBComponent = true,
                HasTypeCComponent = false
            };
            var result = _service.ProcessInventory(worker);

            Assert.True(result);
            Assert.False(worker.HasTypeAComponent);
            Assert.False(worker.HasTypeBComponent);
            Assert.True(worker.HasTypeCComponent);
        }

        [Fact]
        public void ProcessInventory_whenHasOneType_DoesNotProcesses()
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = true,
                HasTypeBComponent = false,
                HasTypeCComponent = false
            };
            var result = _service.ProcessInventory(worker);

            Assert.False(result);
            Assert.True(worker.HasTypeAComponent);
            Assert.False(worker.HasTypeBComponent);
            Assert.False(worker.HasTypeCComponent);
        }

        [Theory]
        [InlineData(true, true, false, ComponentType.TypeA, false)]
        [InlineData(true, true, false, ComponentType.TypeB, false)]
        [InlineData(false, false, true, ComponentType.TypeA, false)]
        [InlineData(false, false, true, ComponentType.TypeB, false)]
        [InlineData(true, false, false, ComponentType.TypeA, false)]
        [InlineData(false, true, false, ComponentType.TypeB, false)]
        [InlineData(false, true, false, ComponentType.TypeA, true)]
        [InlineData(true, false, false, ComponentType.TypeB, true)]
        public void DoesWorkerNeedComponentUrgently_ReturnsCorrectly(bool hasTypeA, bool hasTypeB, bool hasTypeC, ComponentType compType, bool expectedResult)
        {
            var worker = new Worker(1)
            {
                HasTypeAComponent = hasTypeA,
                HasTypeBComponent = hasTypeB,
                HasTypeCComponent = hasTypeC
            };

            var result = _service.DoesWorkerNeedComponentUrgently(worker, compType);

            Assert.Equal(expectedResult, result);
        }
    }
}
