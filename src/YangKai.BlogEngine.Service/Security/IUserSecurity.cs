using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AtomLab.Core;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Service
{
    public interface IUserSecurity
    {
        bool Login(string loginName, string password);
        User Get(string loginName, string password);
        string GetAvater(User user);
    }
}