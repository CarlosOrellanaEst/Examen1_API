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
                .NotEmpty().WithMessage("El nombre es requerido.")
                .Must((dto, name) => !NameAlreadyExists(RemoveDiacritics(name.ToLower()), dto.Id))
                .WithMessage("Ya existe un curso con ese nombre.");

            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.ImageUrl).NotEmpty();
            RuleFor(c => c.Schedule).NotEmpty();
            RuleFor(c => c.Professor).NotEmpty();
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
