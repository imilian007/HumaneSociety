using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var query =
                from Room in db.Rooms
                where Room.Animal.AnimalId == animalId
                select Room;
                
            foreach(Room room in query)
            {
                return room;
            }

            return null;
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

        internal static Client GetClient(string userName, string password)
        {
            Client returnClient = null;
            var query =
                from client in db.Clients
                where client.UserName == userName & client.Password == password
                select client;

            foreach(Client client in query)
            {
                returnClient = client;
            }

            return returnClient;
        }

        internal static IQueryable<Animal> GetUserAdoptionStatus(Client client)
        {
            throw new NotImplementedException();
        }


        //internal static List<Adoption> GetPendingAdoptions()
        //{
        //    List<Adoption> adaptionList = new List<Adoption>();
        //    using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=HumaneSociety;Integrated Security=SSPI"))
        //    {
        //        conn.Open();

        //        // 1.  create a command object identifying the stored procedure
        //        SqlCommand cmd = new SqlCommand("GetPendingAdoptionsSP", conn);

        //        // 2. set the command object so it knows to execute a stored procedure
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        // 3. add parameter to command, which will be passed to the stored procedure
        //        //cmd.Parameters.Add(new SqlParameter("@UserId", userId));

        //        // execute the command

        //        Adoption adapt = new Adoption();
        //        using (SqlDataReader rdr = cmd.ExecuteReader())
        //        {
        //            // iterate through results, printing each to console
        //            while (rdr.Read())
        //            {
        //                //Console.WriteLine("Product: {0,-35} Total: {1,2}", rdr["ProductName"], rdr["Total"]);

        //                adapt.ApprovalStatus = rdr["ApprovalStatus"].ToString();
        //                adapt.PaymentCollected = Convert.ToBoolean(rdr["PaymentCollected"]);
        //                adaptionList.Add(adapt);
        //            }
        //        }

        //    }
        //    return adaptionList;

        //}

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
            db.SubmitChanges();

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

            foreach(Animal animals in query)
            {
                var listOfShots = animals.AnimalShots.ToList();
                return listOfShots;
            }

            return null;
        }

        internal static void UpdateShot(string shotType, Animal animal)
        {
            AnimalShot animalShot = new AnimalShot();
            var query =
                db.Shots.Where(n => n.Name == shotType).Single();
            animalShot.ShotId = query.ShotId;
            animalShot.DateReceived = new DateTime();
            animalShot.AnimalId = animal.AnimalId;

            db.AnimalShots.InsertOnSubmit(animalShot);
            db.SubmitChanges();
        }
    }
}
