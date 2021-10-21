using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.Service.Common
{
    public interface IEmailService
    {
        void SendEmail(string email, string content);
    }
}