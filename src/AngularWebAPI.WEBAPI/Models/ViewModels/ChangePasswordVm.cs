using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularWebAPI.WEBAPI.Models.ViewModels
{
    public class ChangePasswordVm
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}