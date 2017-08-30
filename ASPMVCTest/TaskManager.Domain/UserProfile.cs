using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain
{
    [Table("UserProfile")]
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
