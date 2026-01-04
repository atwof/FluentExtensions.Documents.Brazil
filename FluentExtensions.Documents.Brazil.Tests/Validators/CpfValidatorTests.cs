using FluentExtensions.Documents.Brazil.Validators;

namespace FluentExtensions.Documents.Brazil.Tests.Validators;

public class CpfValidatorTests
{
    [Theory]
    [InlineData("529.982.247-25")]
    [InlineData("52998224725")]
    [InlineData("123.456.789-09")]
    public void IsValid_Should_Return_True_For_Valid_Cpf(string cpf)
    {
        // Act
        var result = CpfValidator.IsValid(cpf);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("111.111.111-11")]
    [InlineData("00000000000")]
    [InlineData("12345678900")]
    [InlineData("123")]
    public void IsValid_Should_Return_False_For_Invalid_Cpf(string cpf)
    {
        // Act
        var result = CpfValidator.IsValid(cpf);

        // Assert
        Assert.False(result);
    }
}