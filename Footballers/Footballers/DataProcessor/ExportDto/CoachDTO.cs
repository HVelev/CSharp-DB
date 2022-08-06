using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ExportDto
{

    [XmlType("Coach")]
    public class CoachDTO
    {

        public CoachDTO()
        {

            Footballers = new List<FootballerXmlDTO>();

        }

        [XmlElement("CoachName")]
        public string CoachName { get; set; }

        [XmlArray("Footballers")]
        public List<FootballerXmlDTO> Footballers { get; set; }

        [XmlAttribute("FootballersCount")]
        public int FootballersCount { get; set; }

    }

    [XmlType("Footballer")]
    public class FootballerXmlDTO
    {

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Position")]
        public string Position { get; set; }



    }
}
