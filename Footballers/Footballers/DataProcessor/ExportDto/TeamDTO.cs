using System;
using System.Collections.Generic;
using System.Text;

namespace Footballers.DataProcessor.ExportDto
{
    public class TeamDTO
    {

        public TeamDTO()
        {

            Footballers = new List<FootballerDTO>();

        }

        public string Name { get; set; }

        public List<FootballerDTO> Footballers { get; set; }

    }

    public class FootballerDTO
    {

        public string FootballerName { get; set; }

        public string ContractStartDate { get; set; }

        public string ContractEndDate { get; set; }

        public string BestSkillType { get; set; }

        public string PositionType { get; set; }

    }
}
