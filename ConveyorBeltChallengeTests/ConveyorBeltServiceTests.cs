using ConveyorBeltChallenge.Models;
using ConveyorBeltChallenge.Services;

namespace ConveyorBeltChallengeTests
{
    public class ConveyorBeltServiceTests
    {
        private readonly IConveyorBeltService _service = new ConveyorBeltService();

        [Fact]
        public void ConveyorBelt_MoveBelt_ReturnsItemsFIFO()
        {
            var belt = new ConveyorBelt(2);
            Component result;

            result = _service.MoveBelt(belt, new Component(ComponentType.TypeA));
            Assert.Equal(ComponentType.Nothing, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.TypeB));
            Assert.Equal(ComponentType.Nothing, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.Nothing));
            Assert.NotNull(result);
            Assert.Equal(ComponentType.TypeA, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.TypeA));
            Assert.NotNull(result);
            Assert.Equal(ComponentType.TypeB, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.TypeB));
            Assert.NotNull(result);
            Assert.Equal(ComponentType.Nothing, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.Nothing));
            Assert.NotNull(result);
            Assert.Equal(ComponentType.TypeA, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.Nothing));
            Assert.NotNull(result);
            Assert.Equal(ComponentType.TypeB, result.ComponentType);

            result = _service.MoveBelt(belt, new Component(ComponentType.Nothing));
            Assert.NotNull(result);
            Assert.Equal(ComponentType.Nothing, result.ComponentType);
        }

        [Fact]
        public void GetComponentAtPosition_whenOk_returnsComponent()
        {
            var conveyor = new ConveyorBelt(4);
            conveyor.Slots[0] = new Component(ComponentType.TypeB);
            conveyor.Slots[1] = new Component(ComponentType.TypeA);
            conveyor.Slots[2] = new Component(ComponentType.Nothing);
            conveyor.Slots[3] = new Component(ComponentType.TypeC);


            var position1Result = _service.GetComponentAtPosition(conveyor, 1);
            var position2Result = _service.GetComponentAtPosition(conveyor, 2);
            var position3Result = _service.GetComponentAtPosition(conveyor, 3);
            var position4Result = _service.GetComponentAtPosition(conveyor, 4);

            Assert.NotNull(position1Result);
            Assert.NotNull(position2Result);
            Assert.NotNull(position3Result);
            Assert.NotNull(position4Result);
            Assert.Equal(ComponentType.TypeB, position1Result.ComponentType);
            Assert.Equal(ComponentType.TypeA, position2Result.ComponentType);
            Assert.Equal(ComponentType.Nothing, position3Result.ComponentType);
            Assert.Equal(ComponentType.TypeC, position4Result.ComponentType);
        }

        [Fact]
        public void GetComponentAtPosition_whenPositionToLow_throwsException()
        {
            var conveyor = new ConveyorBelt(2);
            conveyor.Slots[0] = new Component(ComponentType.TypeB);
            conveyor.Slots[1] = new Component(ComponentType.TypeA);

            try
            {
                var position1Result = _service.GetComponentAtPosition(conveyor, 0);
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void GetComponentAtPosition_whenPositionToHigh_throwsException()
        {
            var conveyor = new ConveyorBelt(2);
            conveyor.Slots[0] = new Component(ComponentType.TypeB);
            conveyor.Slots[1] = new Component(ComponentType.TypeA);

            try
            {
                var position1Result = _service.GetComponentAtPosition(conveyor, 5);
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void PutComponentAtPosition_whenOk_returnsComponent()
        {
            var conveyor = new ConveyorBelt(4);

            _service.PutComponentAtPosition(conveyor, 1, new Component(ComponentType.TypeB));
            _service.PutComponentAtPosition(conveyor, 2, new Component(ComponentType.TypeA));
            _service.PutComponentAtPosition(conveyor, 3, new Component(ComponentType.Nothing));
            _service.PutComponentAtPosition(conveyor, 4, new Component(ComponentType.TypeC));

            Assert.NotNull(conveyor.Slots[0]);
            Assert.NotNull(conveyor.Slots[1]);
            Assert.NotNull(conveyor.Slots[2]);
            Assert.NotNull(conveyor.Slots[3]);
            Assert.Equal(ComponentType.TypeB, conveyor.Slots[0].ComponentType);
            Assert.Equal(ComponentType.TypeA, conveyor.Slots[1].ComponentType);
            Assert.Equal(ComponentType.Nothing, conveyor.Slots[2].ComponentType);
            Assert.Equal(ComponentType.TypeC, conveyor.Slots[3].ComponentType);
        }

        [Fact]
        public void PutComponentAtPosition_whenPositionTooLow_throwsException()
        {
            var conveyor = new ConveyorBelt(2);

            try
            {
                _service.PutComponentAtPosition(conveyor, 0, new Component(ComponentType.TypeA));
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void PutComponentAtPosition_whenPositionTooHigh_throwsException()
        {
            var conveyor = new ConveyorBelt(2);

            try
            {
                _service.PutComponentAtPosition(conveyor, 5, new Component(ComponentType.TypeA));
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void PutComponentAtPosition_whenComponentNull_throwsException()
        {
            var conveyor = new ConveyorBelt(2);

            try
            {
                _service.PutComponentAtPosition(conveyor, 1, null);
            }
            catch (Exception)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }
    }
}
