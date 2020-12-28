using Shopping.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo.IRepo
{
    public interface IEmailRepo
    {
        Task Send(string emailAddress, string body, EmailOptionsDto emailOptions);
    }
}
