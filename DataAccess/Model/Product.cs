using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
   public class Product
    {
        public Product()
        {
            CreatedDate = DateTime.Now;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Column(TypeName = "varchar(50)")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [Column(TypeName = "varchar(500)")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter Quantity")]
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("totalStockPrice")]
        public decimal TotalStockPrice { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedDate { get; set; }

    }
}
