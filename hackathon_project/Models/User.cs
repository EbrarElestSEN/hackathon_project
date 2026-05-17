using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Models;

[Index("Email", Name = "UQ__Users__A9D10534A61AAB89", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string Dormitory { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Budget { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
