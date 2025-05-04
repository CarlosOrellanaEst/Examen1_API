using FluentValidation;
using examenAPI.Dtos.Student;
using examenAPI.Data;
using System.Linq;

namespace examenAPI.Validators
{
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator(AppDbContext context)
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("El correo es obligatorio")
                .EmailAddress().WithMessage("Formato de correo inválido")
                .Must(email => !context.Students.Any(s => s.Email == email))
                .WithMessage("Este correo ya está registrado");

            RuleFor(s => s.Phone)
                .NotEmpty().WithMessage("El teléfono es obligatorio")
                .Must(phone => !context.Students.Any(s => s.Phone == phone))
                .WithMessage("Este teléfono ya está registrado");

            RuleFor(s => s.CourseId)
                .GreaterThan(0).WithMessage("Debe asignarse un curso válido");
        }
    }
}
