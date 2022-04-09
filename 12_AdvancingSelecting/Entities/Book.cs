using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_AdvancingSelecting.Entities
{
    public class Book
    {
        [Column("BookId")]
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public decimal Price { get; set; }

        public Guid PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual PriceOffer PriceOffer { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }


    }
}
