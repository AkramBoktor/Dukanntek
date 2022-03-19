using System;
using FluentValidation;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.ViewModels.General;

namespace Dukkantek.Service.Validators.General
{
    public class RefreshTokenRequestVmValidator : AbstractValidator<RefreshTokenRequestVm>
    {
        public RefreshTokenRequestVmValidator()
        {
           
                RuleFor(u => u).NotNull().OnAnyFailure(x => throw new ArgumentNullException("لا يمكن ان يكون فارغ"));

                RuleFor(u => u.UserId)
                 .Cascade(CascadeMode.Stop)
                 .NotNull().WithMessage("UserId Can't Be Null")
                 .NotEmpty().WithMessage("UserId Can't Be Empty");

                RuleFor(u => u.RefreshToken)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("RefreshToken Can't Be Null")
                    .NotEmpty().WithMessage("RefreshToken Can't Be Empty");

            
        }
    }
}
