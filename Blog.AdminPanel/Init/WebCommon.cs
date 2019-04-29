using Blog.AdminPanel.Models;
using Blog.Common;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.AdminPanel.Init
{
   public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            BlogUser user = CurrentSession.User;

            if (user != null)
                return user.Username;
            else
                return "system";
        }
    }
}