using AutoMapper;
using ChildHealthBook.Common.Identity.DTOs;
using Common.Identity.Setup;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IMapper mapper, UserManager<User> userManager,
            ILogger<AccountsController> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("user/getUsers")]
        public ActionResult<IEnumerable<UserData>> GetUsers()
        {
            _logger.LogInformation("Fetching all users from identity in AccountsController...");
            return Ok(_mapper.Map<IEnumerable<UserData>>(_userManager.Users.Where(u => u.AccountType == "Parent").ToList()));
        }

        [HttpGet]
        [Route("user/getUsers/{userId}")]
        public ActionResult<UserData> GetUserById(Guid userId)
        {
            _logger.LogInformation("Fetching user by id from AccountsController...");
            return Ok(_mapper.Map<IEnumerable<UserData>>(_userManager.Users.Where(u => u.Id == userId).FirstOrDefault()));
        }
        /// <summary>
        /// This POST method registers API user
        /// </summary>
        /// <param name="userUpsertDTO">User credentials passed in UserRegisterDTO</param>
        /// <param name="confirmPassword">Passwprd string</param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/registerParent")]
        public async Task<IActionResult> RegisterNewParent(ParentRegisterDTO parentUpsertDto)
        {
            var errorMessage = new StringBuilder();

            if (parentUpsertDto.UserCredentials.Password == parentUpsertDto.UserCredentials.ConfirmPassword)
            {
                var user = _mapper.Map<ParentRegisterDTO, User>(parentUpsertDto);
                user.AccountType = "Parent";
                var output = await _userManager.CreateAsync(user, parentUpsertDto.UserCredentials.Password);

                if (output.Succeeded)
                {
                    _logger.LogInformation($"Successfully registered new parent with username {parentUpsertDto.UserCredentials.UserName}.");
                    return Ok($"Account {user.UserName} created");
                }

                foreach (var error in output.Errors)
                {
                    errorMessage.Append($"{error.Description} ");
                }

                _logger.LogWarning($"Validation error in AccountsController. message: {errorMessage}");
                return ValidationProblem(errorMessage.ToString());
            }

            _logger.LogWarning($"Validation error in AccountsController, passwords does not match.");
            return ValidationProblem("Passwords are not the same");
        }

        /// <summary>
        /// This POST method registers API user
        /// </summary>
        /// <param name="userUpsertDTO">User credentials passed in UserRegisterDTO</param>
        /// <param name="confirmPassword">Passwprd string</param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/registerScientist")]
        public async Task<IActionResult> RegisterNewScientist(UserRegisterDTO userUpsertDto)
        {
            var errorMessage = new StringBuilder();

            if (userUpsertDto.Password == userUpsertDto.ConfirmPassword)
            {
                var user = _mapper.Map<UserRegisterDTO, User>(userUpsertDto);
                user.AccountType = "Scientist";
                user.Name = "Albert";
                user.Surname = "Einstein";
                user.DateOfBirth = new System.DateTime(2000, 1, 1);
                user.Phone = "123 456 789";
                var output = await _userManager.CreateAsync(user, userUpsertDto.Password);
                if (output.Succeeded)
                {
                    _logger.LogInformation($"Successfully registered new scientist with username: {userUpsertDto.UserName}");
                    return Ok($"Account {user.UserName} created");
                }

                foreach (var error in output.Errors)
                {
                    errorMessage.Append($"{error.Description} ");
                }

                _logger.LogWarning($"Validation error in AccountsController. message: {errorMessage}");
                return ValidationProblem(errorMessage.ToString());
            }

            _logger.LogWarning($"Validation error in AccountsController, passwords does not match.");
            return ValidationProblem("Passwords are not the same");
        }

    }
}
