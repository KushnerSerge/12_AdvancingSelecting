using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_AdvancingSelecting.Entities
{
    public class Publisher 
    {
        [Column("PublisherId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
