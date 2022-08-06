namespace Footballers.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {

            var coaches = context.Coaches.Include(x => x.Footballers).Where(x => x.Footballers.Count >= 1);

            var list = new List<CoachDTO>();

            foreach (var coach in coaches.OrderByDescending(x => x.Footballers.Count).ThenBy(x => x.Name))
            {

                CoachDTO coachDTO = new CoachDTO()
                {

                    CoachName = coach.Name

                };

                foreach (var footballer in coach.Footballers.OrderBy(x => x.Name))
                {

                    FootballerXmlDTO footballerDTO = new FootballerXmlDTO()
                    {

                        Name = footballer.Name,

                        Position = footballer.PositionType.ToString()

                    };

                    coachDTO.Footballers.Add(footballerDTO);

                }

                coachDTO.FootballersCount = coachDTO.Footballers.Count;

                list.Add(coachDTO);

            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<CoachDTO>), new XmlRootAttribute("Coaches"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);
            serializer.Serialize(writer, list, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {

            var teamIds = (from team in context.Teams
                        join teamFootballer in context.TeamsFootballers on team.Id equals teamFootballer.TeamId
                        join footballer in context.Footballers on teamFootballer.FootballerId equals footballer.Id
                        where footballer.ContractStartDate >= date
                        select team.Id).Distinct().ToList();

            var teams = new List<Team>();

            foreach (var team in teamIds)
            {

                teams.Add(context.Teams.Include(x => x.TeamsFootballers).ThenInclude(x => x.Footballer).First(x => x.Id == team));


            }

            var newTeams = teams.Select(x => new
            {

                x.Name,

                Footballers = x.TeamsFootballers.Where(x => x.Footballer.ContractStartDate >= date).ToList()

            }).ToList();

            newTeams = newTeams.OrderByDescending(x => x.Footballers.Count).ThenBy(x => x.Name).ToList();

            var listOfTeams = new List<TeamDTO>();

            foreach (var team in newTeams)
            {
                TeamDTO newTeam = new TeamDTO()
                {

                    Name = team.Name

                };


                foreach (var footballer in team.Footballers.OrderByDescending(x => x.Footballer.ContractEndDate).ThenBy(x => x.Footballer.Name))
                {

                    FootballerDTO footballerDTO = new FootballerDTO()
                    {

                        FootballerName = footballer.Footballer.Name,

                        ContractEndDate = footballer.Footballer.ContractEndDate.ToString("MM/dd/yyyy"),

                        ContractStartDate = footballer.Footballer.ContractStartDate.ToString("MM/dd/yyyy"),

                        BestSkillType = footballer.Footballer.BestSkillType.ToString(),

                        PositionType = footballer.Footballer.PositionType.ToString()

                    };

                    newTeam.Footballers.Add(footballerDTO);

                }

                listOfTeams.Add(newTeam);

            }

            listOfTeams = listOfTeams.Take(5).ToList();

            var json = JsonConvert.SerializeObject(listOfTeams, Formatting.Indented);

            return json;

        }
    }
}
