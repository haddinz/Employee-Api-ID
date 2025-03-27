using EmployeeApi.DTO.Request;
using FluentValidation;

namespace EmployeeApi.DTO.Validator;

public class EmployeeReqValidator : AbstractValidator<EmployeeReq> {
    public EmployeeReqValidator() {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name Is Required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.");

        RuleFor(x => x.Username)
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");
    }
}