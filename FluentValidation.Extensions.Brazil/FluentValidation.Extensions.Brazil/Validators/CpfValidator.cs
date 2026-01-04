using FluentValidation.Extensions.Brazil.Common;

namespace FluentValidation.Extensions.Brazil.Validators;

/// <summary>
/// Provides validation logic for Brazilian CPF (Cadastro de Pessoas Físicas).
/// Implements official Receita Federal rules, including
/// verification digit calculation and invalid sequence checks.
/// </summary>
public static class CpfValidator
{
    /// <summary>
    /// Weight sequence used to calculate the first CPF verification digit (10 → 2).
    /// </summary>
    private static int[] ValidDigitsFirstValidation = [10, 9, 8, 7, 6, 5, 4, 3, 2];
    
    /// <summary>
    /// Weight sequence used to calculate the second CPF verification digit (11 → 2).
    /// </summary>
    private static int[] ValidDigitsSecondValidation = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
    
    /// <summary>
    /// Determines whether the provided value is a valid CPF.
    /// Performs format normalization, repeated-digit checks, and verification digit validation.
    /// </summary>
    /// <param name="cpf">
    /// The CPF value to validate.
    /// </param>
    /// <returns>
    /// True if the CPF is valid; otherwise, false.
    /// </returns>
    public static bool IsValid(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        // Normalize input by keeping only numeric characters
        var onlyDigits = new string(cpf.Where(char.IsDigit).ToArray());

        // CPF must contain exactly 11 digits
        if (onlyDigits.Length != 11) return false;
        
        // Reject CPFs composed of the same digit
        if (onlyDigits.Distinct().Count() == 1) return false;
        
        // First 9 digits are the base used to calculate the verification digits
        var baseDigits = onlyDigits[..9];

        // Calculate the first verification digit
        var firstDigit = CalculateDigit(baseDigits);
        
        // Calculate the second verification digit using the base digits plus the first calculated digit
        var secondDigit = CalculateDigit(baseDigits + firstDigit);
        
        // Create a variable to readability
        var expectedDigits = $"{firstDigit}{secondDigit}";
        
        // Validate that the CPF ends with the calculated verification digits (last two characters)
        return onlyDigits.EndsWith(expectedDigits);
    }

    /// <summary>
    /// Calculates a CPF verification digit using modulo-11 and decreasing weight sequence.
    /// </summary>
    /// <param name="digits">
    /// The numeric string used to calculate the digit.
    /// </param>
    /// <returns>
    /// The calculated verification digit.
    /// </returns>
    private static int CalculateDigit(string digits)
    {
        var length = digits.Length;
        
        var sum = digits
            .Select((digitChar, index) => (digitChar - '0') * (length + 1 - index))
            .Sum();

        var result = sum % Constants.Modulus;
        return result < 2 ? 0 : Constants.Modulus - result;
    }
}