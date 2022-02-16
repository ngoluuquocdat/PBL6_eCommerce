using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Services.Emails
{
    public interface IEmailService
    {
        void SendEmail(string email, string content);
    }
}