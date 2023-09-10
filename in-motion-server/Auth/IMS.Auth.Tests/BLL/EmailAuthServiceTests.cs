using FluentAssertions;

namespace IMS.Auth.Tests.BLL;

public class EmailAuthServiceTests
{
    // Test test to check if working 
    [Fact]
    protected void Test()
    {
        // Arrange 
        var booleandValue = true;
        
        // Act
        booleandValue = false;
        
        // Assert
        booleandValue.Should().Be(false);
    }

    [Fact]
    protected void ConfirmRegisterWithEmailCorrectData()
    {
        
    }
}