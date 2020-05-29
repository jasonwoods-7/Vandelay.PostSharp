using FluentAssertions;
using Xunit;

namespace Vandelay.PostSharp.Tests
{
    public interface IFoo {}

    public class Foo : IFoo {}

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange

            // Act
            var foos = Import.Many<IFoo>("");

            // Assert
            foos.Should().NotBeNullOrEmpty();
        }
    }
}
