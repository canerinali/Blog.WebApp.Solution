﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.AdminPanel.ViewModels
{
    public class OkViewModel: NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "İşlem Başarılı.";
        }
    }
}