using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_AdvancingSelecting.Entities
{
    public class PriceOffer
    {
        public Guid Id { get; set; }
        public decimal NewPrice { get; set; }

        public string PromotionalText { get; set; }

        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
