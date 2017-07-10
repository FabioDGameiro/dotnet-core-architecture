using Project.Domain.Models;
using Xunit;

namespace Project.Domain.Tests.EntityValidationsTests
{
    public class UserValidationsShould
    {
        [Fact]
        public void InvalidateWhenNotGivingAName()
        {
            // Arrange
            var user = new User
            {
                Name = string.Empty
            };

            // Act
            var result = user.IsValid();

            // Assert
            Assert.True(result == false);
        }

        [Fact]
        public void InvalidateWhenNotGivingAEmail()
        {
            // TODO
        }

        [Fact]
        public void InvalidateWhenPassingInvalidEmail()
        {
            // TODO
        }
    }
}
