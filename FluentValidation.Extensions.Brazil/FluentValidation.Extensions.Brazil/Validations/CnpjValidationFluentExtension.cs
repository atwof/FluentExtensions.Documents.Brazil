using FluentValidation.Extensions.Brazil.Validators;

namespace FluentValidation.Extensions.Brazil.Validations;

/// <summary>
/// FluentValidation extension methods for validating Brazilian CNPJ.
/// </summary>
public static class CnpjValidationFluentExtension
{
    /// <summary>
    /// Validates whether the provided value is a valid CNPJ.
    /// Supports both numeric and alphanumeric CNPJ formats
    /// according to Receita Federal rules (including check digits).
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The FluentValidation rule builder.
    /// </param>
    /// <returns>
    /// A rule builder options instance that applies CNPJ validation.
    /// </returns>
    public static IRuleBuilderOptions<T, string?> IsValidCnpj<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(CnpjValidator.IsValid)
            .WithMessage("{PropertyName} must be a valid CNPJ.");
    }
}