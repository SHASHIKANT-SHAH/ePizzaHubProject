﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.Core.Entities
{
    public partial class ItemType
    {
        public ItemType()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [InverseProperty("ItemType")]
        public virtual ICollection<Item> Items { get; set; }
    }
}