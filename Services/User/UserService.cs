    using App.Repositories;
    using App.Repositories.User;
    using App.Services.User.Create;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Vehicle_ServicesRecord.Services.User;

    namespace App.Services.User
    {
        public class UserService(IUsersRepository usersRepository,IUnitOfWork unitOfWork,IMapper mapper, PasswordHasher<Users> passwordHasher) :IUserService

        {
            public async Task<ServiceResult<List<UserDTO>>> GetAllAsync()
            {
                var users = await usersRepository.GetAllAsync();
                var usersAsDto = mapper.Map<List<UserDTO>>(users);

                return ServiceResult<List<UserDTO>>.Success(usersAsDto);
            }
            public async Task<ServiceResult<UserDTO>> GetByIdAsync(int id)
            {
                var user = await usersRepository.GetByIdAsync(id);
                if (user is null)
                {
                    return ServiceResult<UserDTO>.Fail(
                        $"User with id {id} not found",
                        HttpStatusCode.NotFound);
                }

                var userDto = mapper.Map<UserDTO>(user);
                return ServiceResult<UserDTO>.Success(userDto);
            }
            public async Task<ServiceResult<CreateUserResponse>> CreateAsync(CreateUserRequest request)
            {
                if (await usersRepository.UsernameExistsAsync(request.Username))
                {
                    return ServiceResult<CreateUserResponse>.Fail(
                        "Username already exists",
                        HttpStatusCode.BadRequest);
                }

                if (!string.IsNullOrEmpty(request.Email) &&
                    await usersRepository.EmailExistsAsync(request.Email))
                {
                    return ServiceResult<CreateUserResponse>.Fail(
                        "Email already exists",
                        HttpStatusCode.BadRequest);
                }

                var user = mapper.Map<Users>(request);

                user.CreatedAt = DateTime.UtcNow;
                user.Role = "User";

                user.PasswordHash =
                    passwordHasher.HashPassword(user, request.Password);

                await usersRepository.AddAsync(user);
                await unitOfWork.SaveChangesAsync();

                return ServiceResult<CreateUserResponse>.SuccessAsCreated(
                    new CreateUserResponse(user.Id),
                    $"api/users/{user.Id}");
            }
            public async Task<Users?> GetByUsernameOrEmailAsync(string usernameOrEmail)
            {
                return await usersRepository.GetByUsernameOrEmailAsync(usernameOrEmail);
            }
            public async Task<bool> UsernameExistsAsync(string username)
            {
                return await usersRepository.UsernameExistsAsync(username);
            }

            public async Task<bool> EmailExistsAsync(string email)
            {
                return await usersRepository.EmailExistsAsync(email);
            }
        }
    }
