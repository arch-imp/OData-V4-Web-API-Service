using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace WebAPIODataV4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Person.ContactType")]
    public partial class ContactType
    {
        public static class ContactTypeExpressions
        {
            public static readonly Expression<Func<ContactType, DateTime>> ModifiedDate = c => c.LastModifiedOnInternal;
        }

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public ContactType()
        {
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
        }

        public int ContactTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTimeOffset ModifiedDate
        {
            get => new DateTimeOffset(LastModifiedOnInternal);
            set => LastModifiedOnInternal = value.DateTime;
        }

        private DateTime LastModifiedOnInternal { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }
    }
}
