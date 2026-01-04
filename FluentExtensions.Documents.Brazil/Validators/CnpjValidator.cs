using FluentExtensions.Documents.Brazil.Common;

namespace FluentExtensions.Documents.Brazil.Validators;

/// <summary>
/// Provides validation logic for Brazilian CNPJ.
/// Supports both numeric and alphanumeric formats
/// according to Receita Federal rules (2026+).
/// </summary>
public static class CnpjValidator
{
    /// <summary>
    /// Numeric offset used when converting alphabetic characters to their numeric value for check-digit calculation
    /// </summary>
    private const short FirstValueLetterToCalculateDV = 17;
    
    /// <summary>
    /// Weight sequence used to calculate the first verification digit of the CNPJ.
    /// </summary>
    private static int[] ValidDigitsFirstValidation = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    
    /// <summary>
    /// Weight sequence used to calculate the second verification digit of the CNPJ.
    /// </summary>
    private static int[] ValidDigitsSecondValidation => [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    /// <summary>
    /// Determines whether the provided value is a valid CNPJ.
    /// Performs format normalization and verification digit validation.
    /// </summary>
    /// <param name="cnpj">
    /// The CNPJ value to validate.
    /// </param>
    /// <returns>
    /// True if the CNPJ is valid; otherwise, false.
    /// </returns>
    public static bool IsValid(string? cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return false;
            
        // Normalize input:
        // // - keep only letters and digits
        // // - convert letters to uppercase
        var onlyDigits = new string(cnpj
            .Where(char.IsLetterOrDigit)
            .Select(char.ToUpperInvariant)
            .ToArray());

        // CNPJ must have exactly 14 characters
        if (onlyDigits.Length != 14) return false;
        
        // The last two characters must be numeric
        if (!onlyDigits[^2..].All(char.IsDigit)) return false;
        
        // All digits are the same
        if (onlyDigits.Distinct().Count() == 1) return false;
        
        // First 12 characters (base CNPJ)
        var firstDigits = onlyDigits[..12];

        // Calculate the first CNPJ verification digit using the first 12 characters and the first weight sequence  
        var firstDigit = CalculateDigit(firstDigits, ValidDigitsFirstValidation);
        
        // Calculate the second CNPJ verification digit using the first 12 characters plus the first calculated digit and the second weight sequence
        var secondDigit = CalculateDigit(firstDigits + firstDigit, ValidDigitsSecondValidation);

        // Create a variable to readability
        var expectedDigits = $"{firstDigit}{secondDigit}";
            
        // Validate that the CNPJ ends with the calculated verification digits (last two characters)
        return onlyDigits.EndsWith(expectedDigits);
    }
    
    /// <summary>
    /// Calculates a CNPJ verification digit using modulo-11 and a specific weight sequence.
    /// </summary>
    private static int CalculateDigit(string digits, int[] weights)
    {
        var sum = digits
            .Select(CharToValue)
            .Zip(weights, (digit, weight) => digit * weight)
            .Sum();

        var remainder = sum % Constants.Modulus;
        return remainder < 2 ? 0 : Constants.Modulus - remainder;
    }
    
    /// <summary>
    /// Converts a CNPJ character to its numeric value.
    /// </summary>
    private static int CharToValue(char digit)
    {
        if (char.IsDigit(digit)) return digit - '0';
        return digit - 'A' + FirstValueLetterToCalculateDV;
    }
}