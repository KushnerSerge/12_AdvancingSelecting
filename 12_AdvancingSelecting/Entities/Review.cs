using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_AdvancingSelecting.Entities
{
    public class Review 
    {
        [Column("ReviewId")]
        public Guid Id { get; set; }
        public string VoterName { get; set; }

        public short NumStars { get; set; }

        public string Comment { get; set; }

        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
