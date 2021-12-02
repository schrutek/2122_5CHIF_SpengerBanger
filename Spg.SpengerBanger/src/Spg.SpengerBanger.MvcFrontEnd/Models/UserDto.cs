using System.Collections.Generic;

namespace Spg.SpengerBanger.MvcFrontEnd.Models
{
    public class UserDto
    {
        public string SelectedEMail { get; set; }
        public List<string> EMails { get; init; }
    }
}
