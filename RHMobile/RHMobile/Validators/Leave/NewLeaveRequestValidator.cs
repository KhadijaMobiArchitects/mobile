using System;
using FluentValidation;
using XForms.ViewModels;

namespace XForms.Validators.Leave
{
    public class NewLeaveRequestValidator : AbstractValidator<NewLeaveRequestViewModel>
    {
        public NewLeaveRequestValidator()
        {
            RuleFor(vm => vm.SelectedProjet)
                .NotNull();
            RuleFor(vm => vm.SelectedREFTypeLeave)
                .NotNull();
            RuleFor(vm => vm.SelectedSituationProject)
                .NotNull();
            RuleFor(vm => vm.StartDate)
                .NotEqual(DateTime.Now);
            RuleFor(vm => vm.EndDate)
                .NotEqual(DateTime.Now);
        }
    }
}
