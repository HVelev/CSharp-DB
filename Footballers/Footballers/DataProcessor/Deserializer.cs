namespace Footballers.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {

            var sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(CoachDTO[]), new XmlRootAttribute("Coaches"));

            CoachDTO[] coaches;

            using (StringReader reader = new StringReader(xmlString))
            {
                coaches = (CoachDTO[])serializer.Deserialize(reader);
            }

            foreach (var coach in coaches)
            {

                if (!IsValid(coach))
                {

                    sb.AppendLine(ErrorMessage);

                    continue;

                }

                Coach coachDb = new Coach()
                {

                    Name = coach.Name,

                    Nationality = coach.Nationality

                };

                coachDb = context.Coaches.Add(coachDb).Entity;

                foreach (var footballer in coach.Footballers)
                {

                    if (!IsValid(footballer))
                    {

                        sb.AppendLine(ErrorMessage);

                        continue;

                    }
                    else if (footballer.ContractStartDate > footballer.ContractEndDate)
                    {

                        sb.AppendLine(ErrorMessage);

                        continue;

                    }

                    Footballer footballerDb = new Footballer()
                    {

                        Name = footballer.Name,

                        ContractStartDate = footballer.ContractStartDate,

                        ContractEndDate = footballer.ContractEndDate,

                        PositionType = footballer.PosType,

                        BestSkillType = footballer.SkillType,

                        CoachId = coachDb.Id,

                        Coach = coachDb

                    };

                    footballerDb = context.Footballers.Add(footballerDb).Entity;

                }

                sb.AppendLine(String.Format(SuccessfullyImportedCoach, coach.Name, coachDb.Footballers.Count));

            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }
        public static string ImportTeams(FootballersContext context, string jsonString)
        {

            TeamDTO[] teams = JsonConvert.DeserializeObject<TeamDTO[]>(jsonString);

            teams = teams.Select(x => new TeamDTO
            {

                Name = x.Name,

                Nationality = x.Nationality,

                Trophies = x.Trophies,

                Footballers = x.Footballers.Distinct().ToList()

            }).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var team in teams)
            {

                if (!IsValid(team))
                {

                    sb.AppendLine(ErrorMessage);

                    continue;

                }

                Team teamDb = new Team()
                {

                    Name = team.Name,

                    Nationality = team.Nationality,

                    Trophies = team.Trophies
                };

                teamDb = context.Teams.Add(teamDb).Entity;

                foreach (var footballer in team.Footballers)
                {

                    var footballerDb = context.Footballers.FirstOrDefault(x => x.Id == footballer);

                    if (footballerDb == null)
                    {

                        sb.AppendLine(ErrorMessage);

                        continue;

                    }

                    var footballerTeam = new TeamFootballer()
                    {

                        TeamId = teamDb.Id,

                        Team = teamDb,

                        FootballerId = footballerDb.Id,

                        Footballer = footballerDb

                    };

                    footballerTeam = context.TeamsFootballers.Add(footballerTeam).Entity;


                }

                sb.AppendLine(String.Format(SuccessfullyImportedTeam, teamDb.Name, teamDb.TeamsFootballers.Count));

            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
