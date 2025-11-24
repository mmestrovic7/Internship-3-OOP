using Airport.Classes;
using Airport.Managers;

namespace Airport.Helpers;

public class InitializeData
{
    public static void Seed(
        PassengerManager passengerManager,
        PlanesManager planesManager,
        FlightsManager flightsManager,
        CrewManager crewManager)
    {
        SeedCrewMembers(crewManager);
        SeedCrews(crewManager);
        SeedPlanes(planesManager);
        SeedFlights(flightsManager, planesManager, crewManager);
        SeedPassengers(passengerManager, flightsManager);
    }
    private static void SeedCrewMembers(CrewManager crewManager)
        {
            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Ivan",
                LastName = "Meštrović",
                BirthDay = new DateTime(1975,06,30),
                Gender = Gender.Male,
                Position = CrewPosition.Pilot
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Marko",
                LastName = "Marović",
                BirthDay = new DateTime(1985,05,05),
                Gender = Gender.Male,
                Position = CrewPosition.Pilot
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Ana",
                LastName = "Udiljak",
                BirthDay = new DateTime(1992,11,30),
                Gender = Gender.Female,
                Position = CrewPosition.Copilot
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Lucija",
                LastName = "Topić",
                BirthDay = new DateTime(1995,12,09),
                Gender = Gender.Female,
                Position = CrewPosition.Copilot
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Maja",
                LastName = "Babić",
                BirthDay = new DateTime(1976,12,10),
                Gender = Gender.Female,
                Position = CrewPosition.FlightAttendant
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Luka",
                LastName = "Petrović",
                BirthDay = new DateTime(1985,02,05),
                Gender = Gender.Male,
                Position = CrewPosition.FlightAttendant
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Sara",
                LastName = "Jurić",
                BirthDay = new DateTime(1988,02,05),
                Gender = Gender.Female,
                Position = CrewPosition.FlightAttendant
            });

            crewManager.AddCrewMember(new CrewMember
            {
                FirstName = "Tomislav",
                LastName = "Knežević",
                BirthDay = new DateTime(1980,01,09),
                Gender = Gender.Male,
                Position = CrewPosition.FlightAttendant
            });
        }

        private static void SeedCrews(CrewManager crewManager)
        {
            var allMembers = crewManager.GetAllCrewMembers();
            
            var crew1 = new Crew { Name = "Posada Alpha" };
            crew1.PilotId = allMembers[0].Id;
            crew1.CopilotId = allMembers[2].Id;
            crew1.FlightAttendantIds.Add(allMembers[4].Id);
            crew1.FlightAttendantIds.Add(allMembers[5].Id);
            crewManager.AddCrew(crew1);

            var crew2 = new Crew { Name = "Posada Bravo" };
            crew2.PilotId = allMembers[1].Id;
            crew2.CopilotId = allMembers[3].Id;
            crew2.FlightAttendantIds.Add(allMembers[6].Id);
            crew2.FlightAttendantIds.Add(allMembers[7].Id);
            crewManager.AddCrew(crew2);
        }

        private static void SeedPlanes(PlanesManager planesManager)
        {
            var plane1 = new Plane
            {
                Name = "Boeing 737-800",
                ManufactureYear = 2015
            };
            plane1.SeatsByCategory[SeatCategory.Standard] = 150;
            plane1.SeatsByCategory[SeatCategory.Business] = 30;
            planesManager.AddPlane(plane1);

            var plane2 = new Plane
            {
                Name = "Airbus A320",
                ManufactureYear = 2018
            };
            plane2.SeatsByCategory[SeatCategory.Standard] = 120;
            plane2.SeatsByCategory[SeatCategory.Business] = 20;
            plane2.SeatsByCategory[SeatCategory.VIP] = 10;
            planesManager.AddPlane(plane2);

            var plane3 = new Plane
            {
                Name = "Embraer E195",
                ManufactureYear = 2020
            };
            plane3.SeatsByCategory[SeatCategory.Standard] = 100;
            plane3.SeatsByCategory[SeatCategory.Business] = 20;
            planesManager.AddPlane(plane3);
            var plane4 = new Plane
            {
                Name = "Kapacitet test",
                ManufactureYear = 2020
            };
            plane4.SeatsByCategory[SeatCategory.Standard] = 2;
            planesManager.AddPlane(plane4);
        }

        private static void SeedFlights(FlightsManager flightsManager, PlanesManager planesManager, CrewManager crewManager)
        {
            var planes = planesManager.GetAllPlanes();
            var crews = crewManager.GetAllCrews();

            var flight1 = new Flight
            {
                FlightNumber = "OU101",
                DepartureLocation = "Zagreb",
                ArrivalLocation = "London",
                DepartureTime = DateTime.Now.AddDays(5).AddHours(10),
                ArrivalTime = DateTime.Now.AddDays(5).AddHours(12).AddMinutes(30),
                Distance = 1450,
                PlaneId = planes[0].Id,
                CrewId = crews[0].Id
            };
            flight1.Duration = flight1.ArrivalTime - flight1.DepartureTime;
            flightsManager.AddFlight(flight1);

            var flight2 = new Flight
            {
                FlightNumber = "OU202",
                DepartureLocation = "Split",
                ArrivalLocation = "Paris",
                DepartureTime = DateTime.Now.AddDays(0).AddHours(10),
                ArrivalTime = DateTime.Now.AddDays(0).AddHours(12).AddMinutes(15),
                Distance = 1250,
                PlaneId = planes[1].Id,
                CrewId = crews[1].Id
            };
            flight2.Duration = flight2.ArrivalTime - flight2.DepartureTime;
            flightsManager.AddFlight(flight2);

            var flight3 = new Flight
            {
                FlightNumber = "OU303",
                DepartureLocation = "Dubrovnik",
                ArrivalLocation = "Rome",
                DepartureTime = DateTime.Now.AddDays(7).AddHours(9),
                ArrivalTime = DateTime.Now.AddDays(7).AddHours(10).AddMinutes(20),
                Distance = 520,
                PlaneId = planes[2].Id,
                CrewId = crews[0].Id
            };
            flight3.Duration = flight3.ArrivalTime - flight3.DepartureTime;
            flightsManager.AddFlight(flight3);

            var flight4 = new Flight
            {
                FlightNumber = "OU404",
                DepartureLocation = "Zagreb",
                ArrivalLocation = "Berlin",
                DepartureTime = DateTime.Now.AddHours(30),
                ArrivalTime = DateTime.Now.AddHours(31).AddMinutes(45),
                Distance = 760,
                PlaneId = planes[0].Id,
                CrewId = crews[1].Id
            };
            flight4.Duration = flight4.ArrivalTime - flight4.DepartureTime;
            flightsManager.AddFlight(flight4);
            var flight5 = new Flight
            {
                FlightNumber = "OU505",
                DepartureLocation = "Zagreb",
                ArrivalLocation = "Pariz",
                DepartureTime = DateTime.Now.AddHours(30),
                ArrivalTime = DateTime.Now.AddHours(31).AddMinutes(45),
                Distance = 760,
                PlaneId = planes[3].Id,
                CrewId = crews[1].Id
            };
            flight4.Duration = flight5.ArrivalTime - flight5.DepartureTime;
            flightsManager.AddFlight(flight5);
        }

        private static void SeedPassengers(PassengerManager passengerManager, FlightsManager flightsManager)
        {
            passengerManager.RegisterPassenger("Petar", "Perić",  new DateTime(1980,03,06), "petar@test.com", "password123");
            passengerManager.RegisterPassenger("Ivana", "Ivić", new DateTime(2000,11,05), "ivana@test.com", "password123");
            passengerManager.RegisterPassenger("Ante", "Antić", new DateTime(1990,1,05), "ante@test.com", "password123");

            var flights = flightsManager.GetAllFlights();
            if (flights.Count >= 2)
            {
                var passenger1 = passengerManager.Login("petar@test.com", "password123");
                passengerManager.BookFlight(passenger1.Id, flights[0].Id, SeatCategory.Business);
                flightsManager.BookSeat(flights[0].Id, SeatCategory.Business);

                passengerManager.BookFlight(passenger1.Id, flights[1].Id, SeatCategory.Standard);
                flightsManager.BookSeat(flights[1].Id, SeatCategory.Standard);
                
                passengerManager.BookFlight(passenger1.Id, flights[4].Id, SeatCategory.Standard);
                flightsManager.BookSeat(flights[4].Id, SeatCategory.Standard);
            }
        }
}