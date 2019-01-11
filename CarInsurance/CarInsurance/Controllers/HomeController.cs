using CarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class HomeController : Controller
    {

        // GET: ContactUs
        public ActionResult Contact()
        {
            return View();
        }


        private readonly string connectionString =
            @"Data Source=DESKTOP-UDKTPG7\SQLEXPRESS;Initial Catalog=BestPriceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult ViewQuote(string firstName, string lastName, string emailAddress,
            DateTime DOB, int carYear, string carMake, string carModel, string DUI,
            int speedingTickets, string fullCoverage)
        {
            var today = DateTime.Today;
            var age = today.Year - DOB.Year;

            // int age = Convert.ToInt32(DOB);

            var RT = 50;

            // age check
            if (age < 25 && age > 18)
            {
                RT += 25;
            }
            else if (age < 18)
            {
                RT += 100;
            }
            else if (age > 100)
            {
                RT += 25;
            }
            else


            // car year check
            if (carYear < 2000)
            {
                RT += 25;
            }
            else if (carYear > 2015)
            {
                RT += 25;
            }
            else
            {
                RT += 0;
            }


            // car make check
            if (carMake == "porsche")
            {
                RT += 25;
            }


            if (carMake == "porsche" && carModel == "911 carrera")
            {
                RT += 25;
            }
            else
            {
                RT += 0;
            }

            for (int i = 0; i < speedingTickets; i++)
            {
                RT += 10;
            }

            if (DUI == "Yes")
            {
                RT += (RT / 25);
            }
            else
            {
                RT += 0;
            }

            if (fullCoverage == "Yes")
            {
                RT += (RT / 50);
            }
            else
            {
                RT += 0;
            }


            string queryString = @"INSERT INTO BestPriceDb (firstName, lastName, emailAddress, DOB, carYear, carMake, carModel, DUI, speedingTickets, fullCoverage)
                                    VALUES (@firstName, @lastName, @emailAddress, @DOB, @carYear, @carMake, @carModel, @DUI, @speedingTickets, @fullCoverage)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@firstName", SqlDbType.VarChar);
                command.Parameters.Add("@lastName", SqlDbType.VarChar);
                command.Parameters.Add("@emailAddress", SqlDbType.VarChar);
                command.Parameters.Add("@DOB", SqlDbType.Date);
                command.Parameters.Add("@carYear", SqlDbType.Int);
                command.Parameters.Add("@carModel", SqlDbType.VarChar);
                command.Parameters.Add("@carMake", SqlDbType.VarChar);
                command.Parameters.Add("@DUI", SqlDbType.VarChar);
                command.Parameters.Add("@speedingTickets", SqlDbType.Int);
                command.Parameters.Add("@fullcoverage", SqlDbType.VarChar);

                command.Parameters["@firstName"].Value = firstName;
                command.Parameters["@lastName"].Value = lastName;
                command.Parameters["@emailAddress"].Value = emailAddress;
                command.Parameters["@DOB"].Value = DOB;
                command.Parameters["@carYear"].Value = carYear;
                command.Parameters["@carModel"].Value = carModel;
                command.Parameters["@carMake"].Value = carMake;
                command.Parameters["@DUI"].Value = DUI;
                command.Parameters["@speedingTickets"].Value = speedingTickets;
                command.Parameters["@fullCoverage"].Value = fullCoverage;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();



            }
            ViewBag.Quote = RT;
            return View("ViewQuote");
        }



        public ActionResult Admin()
        {
            string queryString = @"SELECT Id, firstName, lastName, emailAddress, DOB, carYear, carMake, carModel, DUI, speedingTickets, fullCoverage from BestPriceDb";

            List<BestPriceQuotes> customers = new List<BestPriceQuotes>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var customer = new BestPriceQuotes();
                    customer.Id = Convert.ToInt32(reader["Id"]);
                    customer.firstName = reader["FirstName"].ToString();
                    customer.lastName = reader["lastName"].ToString();
                    customer.emailAddress = reader["emailAddress"].ToString();
                    /*customer.DOB = Convert.ToInt32(reader["DOB"]);*/
                    customer.carYear = Convert.ToInt32(reader["carYear"]);
                    customer.carMake = reader["carMake"].ToString();
                    customer.carModel = reader["carModel"].ToString();
                    customer.DUI = reader["DUI"].ToString();
                    customer.speedingTickets = Convert.ToInt32(reader["speedingTickets"]);
                    customer.fullCoverage = reader["fullCoverage"].ToString();
                    customers.Add(customer);

                }
            }
            return View(customers);
        }
    }
}
