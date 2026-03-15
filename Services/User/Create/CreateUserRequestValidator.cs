
using App.Repositories.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.User.Create
{
    public class CreateUserRequestValidator:AbstractValidator<CreateUserRequest>
    {
        private readonly IUsersRepository _usersRepository;
        public CreateUserRequestValidator(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir");

            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");

            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
                .Matches("[A-Z]").WithMessage("Şifre en az 1 büyük harf içermelidir")
                .Matches("[a-z]").WithMessage("Şifre en az 1 küçük harf içermelidir")
                .Matches("[0-9]").WithMessage("Şifre en az 1 rakam içermelidir");
        }
    }

    }



