using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Models;

public partial class OrderPool
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Title { get; set; } = null!;

    public int RestaurantId { get; set; }

    [StringLength(100)]
    public string Dormitory { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal CurrentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [InverseProperty("OrderPool")]
    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    [ForeignKey("RestaurantId")]
    [InverseProperty("OrderPools")]
    public virtual Restaurant Restaurant { get; set; } = null!;
}
