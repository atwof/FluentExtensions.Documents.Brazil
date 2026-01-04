using FluentValidation.Extensions.Brazil.Validators;

namespace FluentValidation.Extensions.Brazil.Tests.Validators;

public class CnpjValidatorTests
{
    [Theory]
    [InlineData("12.ABC.345/01DE-35")]
    [InlineData("04.252.011/0001-10")]
    [InlineData("04252011000110")]
    [InlineData("40.688.134/0001-61")]
    public void IsValid_Should_Return_True_For_Valid_Numeric_Cnpj(string cnpj)
    {
        // Act
        var result = CnpjValidator.IsValid(cnpj);

        // Assert
        Assert.True(result);
    }
    
    [Theory]
    [InlineData("11.111.111/1111-11")]
    [InlineData("00000000000000")]
    [InlineData("123")]
    [InlineData(null)]
    [InlineData("")]
    public void IsValid_Should_Return_False_For_Invalid_Cnpj(string cnpj)
    {
        // Act
        var result = CnpjValidator.IsValid(cnpj);

        // Assert
        Assert.False(result);
    }

    [Theory]
    // Example format for alphanumeric CNPJ (structure test)
    // Replace with real valid values when Receita Federal
    // publishes official test samples
    [InlineData("A1B2C3D4E5F600")]
    public void IsValid_Should_Not_Throw_For_Alphanumeric_Cnpj(string cnpj)
    {
        // Act
        var result = CnpjValidator.IsValid(cnpj);

        // Assert
        Assert.False(result); // structure valid, digits probably not
    }
}