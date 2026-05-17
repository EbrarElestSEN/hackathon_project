using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Models;

public partial class Participant
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OrderPoolId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [StringLength(250)]
    public string? OrderNote { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime JoinedAt { get; set; }

    [ForeignKey("OrderPoolId")]
    [InverseProperty("Participants")]
    public virtual OrderPool OrderPool { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Participants")]
    public virtual User User { get; set; } = null!;
}
