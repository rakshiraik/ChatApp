using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Common.ViewModels
{
    public class RoomViewModel
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public int NoOfPeopleAllowed { get; set; }

    }
}
