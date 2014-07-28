using System.ComponentModel.DataAnnotations.Schema;

namespace RWS.Repositories
{
    [Table("OrderRequest")]
    public class OrderRequest
    {
        public int Id { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "text")]
        public string Payload { get; set; }
    }
}