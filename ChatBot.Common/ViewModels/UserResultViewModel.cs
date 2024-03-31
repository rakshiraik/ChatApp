using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Common.ViewModels
{
    public  class UserResultViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string Token { get; set; } = null!;
        public DateTime TokenExpiryDate { get; set; }
    }
}
