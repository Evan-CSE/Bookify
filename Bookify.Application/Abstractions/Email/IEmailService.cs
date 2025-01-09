using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Abstractions.Email
{
    internal interface IEmailService
    {
        Task<bool> SendEmailAsync(Bookify.Domain.Users.Email mail, string subject, string body);
    }
}
