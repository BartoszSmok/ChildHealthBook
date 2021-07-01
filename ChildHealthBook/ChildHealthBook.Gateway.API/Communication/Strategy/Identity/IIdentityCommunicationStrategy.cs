﻿using ChildHealthBook.Common.Identity.DTOs;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Strategy.Identity
{
    public interface IIdentityCommunicationStrategy
    {
        Task<string> GetToken(string url, UserLoginDTO credentials);
        Task RegisterParent(string url, ParentRegisterDTO parentData);

        Task RegisterScientist(string url, UserRegisterDTO userData);
    }
}
