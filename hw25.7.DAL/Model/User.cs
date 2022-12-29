using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace hw25.DAL.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";

        public List<IssuedBook>? IssuedBooks { get; set; }

    }
}
