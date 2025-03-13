using ConveyorBeltChallenge.Services;

namespace ConveyorBeltChallengeTests
{
    public class ComponentServiceTests
    {
        private readonly IComponentService _service = new ComponentService();

        [Fact]
        public void GetRandomComponent_returnsComponent()
        {
            var result = _service.GetRandomComponent();

            Assert.NotNull(result);
        }

    }
}