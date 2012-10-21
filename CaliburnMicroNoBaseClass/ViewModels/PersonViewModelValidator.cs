using FluentValidation;

namespace CaliburnMicroWithNoBaseClass.ViewModels
{
    public class PersonViewModelValidator : AbstractValidator<PersonViewModel>
    {
        public PersonViewModelValidator()
        {
            RuleFor(x => x.FamilyName).NotEmpty();
            RuleFor(x => x.GivenNames).NotEmpty();
        }
    }
}