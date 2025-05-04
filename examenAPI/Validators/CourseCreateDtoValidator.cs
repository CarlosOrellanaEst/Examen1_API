using FluentValidation;
using examenAPI.Dtos.Course;
using examenAPI.Data;
using System.Linq;

namespace examenAPI.Validators
{
    public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidator(AppDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("El nombre del curso es obligatorio")
                .Must(name => !context.Courses.AsEnumerable().Any(c => RemoveDiacritics(c.Name.ToLower()) == RemoveDiacritics(name.ToLower())))
                .WithMessage("Ya existe un curso con ese nombre");

            RuleFor(c => c.Professor)
                .NotEmpty().WithMessage("El nombre del profesor es obligatorio");
        }

        private string RemoveDiacritics(string text)
        {
            return string.Concat(text.Normalize(System.Text.NormalizationForm.FormD)
                .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark))
                .Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
