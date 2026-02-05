using FlashCards.Application.Stacks.GetAll;
using FlashCards.Core.Validation;

namespace FlashCards.UnitTests.ConsoleUITests;

public class MainMenuServiceTests
{
    [Fact]
    public void GetStacks_ShouldReturnNull_WhenResultIsFailure()
    {
        // Arrange
        var result = Result<List<StackNameAndCardCountResponse>>.Failure(Errors.NoStacksExist);

        // Act

    }

    // NOT ACTUALLY TESTABLE!
    //          Would need to have the function take the result as a parameter
}
