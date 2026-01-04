using FluentExtensions.Documents.Brazil.Validators;
using FluentValidation;

namespace FluentExtensions.Documents.Brazil.Validations;

/// <summary>
/// FluentValidation extension methods for validating Brazilian CPF.
/// </summary>
public static class CpfValidationFluentExtension
{
    /// <summary>
    /// Validates whether the provided value is a valid CPF
    /// according to Receita Federal rules, including
    /// verification digit calculation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The FluentValidation rule builder.
    /// </param>
    /// <returns>
    /// A rule builder options instance that applies CPF validation.
    /// </returns>
    public static IRuleBuilderOptions<T, string?> IsValidCpf<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(CpfValidator.IsValid)
            .WithMessage("{PropertyName} must be a valid CPF.");
    }
}