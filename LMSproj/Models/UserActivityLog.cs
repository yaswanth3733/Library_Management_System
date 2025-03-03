using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace LMSproj.Models
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // Borrowed / Returned
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public decimal? FineAmount { get; set; } // Null if not applicable
        public string Description { get; set; }
    }
   }
