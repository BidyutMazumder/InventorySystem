﻿using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string FullName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int LoyaltyPoints { get; set; }
    public bool IsDeleted { get; set; } = false;
}

