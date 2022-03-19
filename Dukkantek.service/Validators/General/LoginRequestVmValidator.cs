using System;
using FluentValidation;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.ViewModels.General;

namespace Dukkantek.Service.Validators.General
{
    public class LoginRequestVmValidator : AbstractValidator<LoginRequestVm>
    {
        public LoginRequestVmValidator()
        {
          
                 RuleFor(u => u).NotNull().OnAnyFailure(x => throw new ArgumentNullException("Can't found the object."));

                RuleFor(u => u.UserName)
                 .Cascade(CascadeMode.Stop)
                 .NotNull().WithMessage("UserName Can't Be Null")
                 .NotEmpty().WithMessage("UserName Can't Be Empty");

                RuleFor(u => u.Password)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Password Can't Be Null")
                   .NotEmpty().WithMessage("Password Can't Be Empty")
                   .MaximumLength(20).WithMessage("Password Should Be Less Than Or Equal 20 Digits")
                   .MinimumLength(3).WithMessage("Password Should Be More Than Or Equal 3 Digits");
            
        }
    }
}
