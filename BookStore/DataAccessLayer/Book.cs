//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DataAccessLayer;
    using System.Web;
    using Models;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Books_AttributesValue = new HashSet<Books_AttributesValue>();
        }
    
        public int ID { get; set; }

        [Required(ErrorMessage = "Title can't be empty: MAX 40 Characters!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author can't be empty!")]
        [Display(Name = "Author")]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Genre can't be empty!")]
        [Display(Name = "Genre")]
        public int GenreID { get; set; }

        [Display(Name = "Image")]
        [NotMapped]
        [ValidateImage]
        public string ImagePath { get; set; }

        [Display(Name = "Pages")]
        public Nullable<int> PageCount { get; set; }

        [Required(ErrorMessage = "Decription can't be empty!")]
        public string Description { get; set; }

        [Display(Name = "Country")]
        public int CountryID { get; set; }
        
        [Required(ErrorMessage = "Price can't be empty!")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Enter Price for this format $XX.XX")]
        public decimal Price { get; set; }
    
        public virtual Author Author { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Books_AttributesValue> Books_AttributesValue { get; set; }
        public virtual Country Country { get; set; }
        public virtual Genre Genre { get; set; }
    }
}