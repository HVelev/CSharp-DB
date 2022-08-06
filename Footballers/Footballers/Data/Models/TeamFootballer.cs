using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {

        [Key]
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public Team Team { get; set; }

        [Key]
        [ForeignKey("Footballer")]
        public int FootballerId { get; set; }

        public Footballer Footballer { get; set; }

    }
}
