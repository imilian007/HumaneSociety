using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HumaneSociety
{
    public static class Query
    {
        public delegate IEnumerable<Employee> RunEmployeeQueries(Employee employee, string function);

        private static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        public static IEnumerable<Employee> Create(Employee employee, string function)
        {
            db.Employees.InsertOnSubmit(employee);
            yield return employee;

        }

        public static IEnumerable<Employee> ReadEmployee(Employee employee, string function)
        {
            return db.Employees.Where(c => c.Equals(employee));
        }


        public static IEnumerable<Employee> Update(Employee employee, string v)
        {
            db.Employees.InsertOnSubmit(employee);
            yield return employee;
        }

        public static IEnumerable<Employee> Delete(Employee employee, string v)
        {
            db.Employees.DeleteOnSubmit(employee);
            yield return employee;
        }


        internal static Room GetRoom(int animalId)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits()
        {
            throw new NotImplementedException();
        }

        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static object GetAnimalByID(int iD)
        {
            throw new NotImplementedException();
        }

        public static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Client GetClient(string userName, string password)
        {

            Client returnClient = null;
            var query =
                from client in db.Clients
                where client.UserName == userName & client.Password == password
                select client;

            foreach (Client client in query)
            {
                returnClient = client;
            }

            return returnClient;
        }

        internal static IQueryable<Animal> GetUserAdoptionStatus(Client client)
        {
            throw new NotImplementedException();
        }



        internal static IEnumerable<Client> RetrieveClients()
        {
            var clientList = db.Clients.ToList();
            return clientList;
        }

        public static List<USState> GetStates()
        {
            var stateList = db.USStates.ToList();
            return stateList;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;
            client.Address.AddressLine1 = streetAddress;
            client.Address.Zipcode = zipCode;
            db.Clients.InsertOnSubmit(client);

        }

        internal static void updateClient(Client client)
        {
            db.SubmitChanges();
        }

        internal static void UpdateEmail(Client client1)
        {
            var query =
                from client in db.Clients
                where client1.ClientId == client.ClientId
                select client;

            foreach (Client client in query)
            {
                client.Email = Console.ReadLine();
            }

            db.SubmitChanges();
        }

        internal static void UpdateUsername(Client client1)
        {
            var query =
                from client in db.Clients
                where client1.ClientId == client.ClientId
                select client;

            foreach (Client client in query)
            {
                client.UserName = Console.ReadLine();
            }
            db.SubmitChanges();
        }

        internal static void UpdateAddress(Client client1)
        {
            var query =
                from client in db.Clients
                where client1.ClientId == client.ClientId
                select client;

            foreach (Client client in query)
            {
                client.Address.AddressLine1 = Console.ReadLine();
            }
        }

        internal static void UpdateFirstName(Client client1)
        {
            var query =
                from client in db.Clients
                where client1.ClientId == client.ClientId
                select client;

            foreach (Client client in query)
            {
                client.FirstName = Console.ReadLine();
            }
            db.SubmitChanges();
        }

        internal static void UpdateLastName(Client client1)
        {
            var query =
                from client in db.Clients
                where client1.ClientId == client.ClientId
                select client;

            foreach (Client client in query)
            {
                client.LastName = Console.ReadLine();
            }
            db.SubmitChanges();
        }

        internal static List<AnimalShot> GetShots(Animal animal)
        {
            var query =
                from animals in db.Animals
                where animal.AnimalId == animals.AnimalId
                select animals;

            foreach (Animal animals in query)
            {
                var listOfShots = animals.AnimalShots.ToList();
                return listOfShots;
            }

            return null;
        }

        internal static void ImportCSVfile(string file)
        {

            string[] animalList = File.ReadAllLines(file);

            var animalData = from list in animalList
                             let data = list.Split(',')
                             select new
                             {
                                 // ID = data[0],
                                 Name = data[1],
                                 SpeciesId = data[2],
                                 Weight = data[3],
                                 Age = data[4],
                                 DietPlanId = data[5],
                                 Demeanor = data[6],
                                 KidFriendly = data[7],
                                 PetFriendly = data[8],
                                 Gender = data[9],
                                 AdoptionStatus = data[10],
                                 EmployeeId = data[11]

                             };
            foreach (var a in animalData)
            {
                Console.WriteLine("[{1}] {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}", a.Name, a.SpeciesId, a.Weight, a.Age, a.DietPlanId, a.Demeanor, a.KidFriendly, a.PetFriendly, a.Gender, a.AdoptionStatus, a.EmployeeId);
            }
            var animalList1 = db.Animals.ToList();
        }

        internal static IEnumerable<Animal> RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            UserInterface.DisplayUserOptions("Animal successfully" + v + "d");
            yield return animal;
        }

        public static int? GetSpecies(string userInput)
        {
            var query = db.Species.Where(s => s.Name == userInput).Single();
            return query.SpeciesId;
        }

        public static int? GetDietPlan(string userInput1)
        {
            var query = db.Animals.Where(a => a.DietPlan.Name == userInput1).Single();
            return query.DietPlanId;

        }

        internal static void AddAnimal(int? speciesId, string name, int? age, string demeanor, bool? kidFriendly, bool? petFriendly, int? weight, int? dietPlanId)
        {
            Animal animal = new Animal();
            animal.SpeciesId = speciesId;
            animal.Name = name;
            animal.Age = age;
            animal.Demeanor = demeanor;
            animal.KidFriendly = kidFriendly;
            animal.PetFriendly = petFriendly;
            animal.Weight = weight;
            animal.DietPlanId = dietPlanId;

        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            return db.Employees.Where(c => c.Equals(employeeNumber) && c.Equals(email)).Single();
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            return db.Employees.Where(e => e.Equals(userName) && e.Equals(password)).Single();
        }
        internal static Employee AddUsernameAndPassword(employee)
        {
            db.Employees.InsertOnSubmit(employee);
            yield return employee;
        }


        internal static bool CheckEmployeeUserNameExist(string username)
        {
            return db.Employees.Where(e => e.Equals(username)).ToLookup();
        }
    }
}
