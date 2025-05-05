using examenAPI.Dtos.Course;
using FluentValidation;
using examenAPI.Data;

namespace examenAPI.Validators
{
    public class CourseUpdateDtoValidator : AbstractValidator<CourseUpdateDto>
    {
        private readonly AppDbContext _context;

        public CourseUpdateDtoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .When(c => c.Name != null);

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria")
                .When(c => c.Description != null);

            RuleFor(c => c.Schedule)
                .NotEmpty().WithMessage("El horario es obligatorio")
                .When(c => c.Schedule != null);

            RuleFor(c => c.Professor)
                .NotEmpty().WithMessage("El profesor es obligatorio")
                .When(c => c.Professor != null);
        }

        private bool NameAlreadyExists(string name, int id)
        {
            return _context.Courses.AsEnumerable().Any(c => RemoveDiacritics(c.Name.ToLower()) == name && c.Id != id);
        }

        private string RemoveDiacritics(string text)
        {
            return string.Concat(text.Normalize(System.Text.NormalizationForm.FormD)
                .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark))
                .Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
