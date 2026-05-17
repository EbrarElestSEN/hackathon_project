using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Models;

public partial class Restaurant
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal MinimumOrderAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DeliveryFee { get; set; }

    [InverseProperty("Restaurant")]
    public virtual ICollection<OrderPool> OrderPools { get; set; } = new List<OrderPool>();
}
