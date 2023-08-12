using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.OrderAggreagate
{
    public class DeliveryMethod : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}