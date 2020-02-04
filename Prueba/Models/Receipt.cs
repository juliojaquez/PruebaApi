using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaAPI.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string Provider { get; set; }
    }
}
