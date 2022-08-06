using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{

    [XmlType("Coach")]
    public class CoachDTO
    {

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public string Nationality { get; set; }

        [XmlArray("Footballers")]
        [Required]
        public List<FootballerDTO> Footballers { get; set; }

    }

    [XmlType("Footballer")]
    public class FootballerDTO
    {

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [XmlElement("ContractStartDate")]
        public string ContractStartDateStr { get; set; }

        public DateTime ContractStartDate => DateTime.ParseExact(ContractStartDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        [Required]
        [XmlElement("ContractEndDate")]
        public string ContractEndDateStr { get; set; }

        public DateTime ContractEndDate => DateTime.ParseExact(ContractEndDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        [Required]
        public int PositionType { get; set; }

        [Required]
        public int BestSkillType { get; set; }

        public PositionType PosType => (PositionType)PositionType;

        public BestSkillType SkillType => (BestSkillType)BestSkillType;

    }

}
