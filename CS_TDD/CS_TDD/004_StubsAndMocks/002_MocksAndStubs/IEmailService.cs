﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TDD._004_StubsAndMocks._002_MocksAndStubs
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
