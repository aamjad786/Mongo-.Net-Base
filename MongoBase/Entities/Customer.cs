
using System.ComponentModel.DataAnnotations.Schema;

namespace MongoBase.Entities
{
    [Table("customer")]
    public class Customer : MongoBaseEntity<string>
    {
        public string CName { get; set; }
        public int? CAge { get; set; }
        public DateTime? CDoB { get; set; }

        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var customer = obj as Customer;
            if (customer?.Id == null || Id == null) return false;
            return EqualityComparer<string>.Default.Equals(Id, customer.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return "Customer{" +
                    $"ID='{Id}'" +
                    $", CName='{CName}'" +
                    $", CAge='{CAge}'" +
                    $", CDoB='{CDoB}'" +
                    "}";
        }
    }
}
