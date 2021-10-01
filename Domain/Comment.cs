using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Treator Creator { get; set; }
        public bool VisibleForPatient { get; set; }

        public Comment(int id, string description, DateTime creationDateTime, Treator creator, bool visibleForPatient)
        {
            Id = id;
            Description = description;
            CreationDateTime = creationDateTime;
            Creator = creator;
            VisibleForPatient = visibleForPatient;
        }

        public Comment()
        {

        }
    }
}
