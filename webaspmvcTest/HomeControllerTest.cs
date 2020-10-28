using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using webapsmvc.Models;
using Xunit;

namespace webaspmvcTest
{
    public class HomeControllerTest : Xunit.Sdk.BeforeAfterTestAttribute
    {
        [Fact]
        public async System.Threading.Tasks.Task BuyAsync()
        {
            Order order = new Order();
            Order restoredPerson = null;
            using (FileStream fs = new FileStream("order.json", FileMode.OpenOrCreate))
            {
                restoredPerson = await JsonSerializer.DeserializeAsync<Order>(fs);
                Console.WriteLine($"OrderId: {restoredPerson.OrderId}  User: {restoredPerson.User} Address: {restoredPerson.Address} ContactPhone: {restoredPerson.ContactPhone} PhoneId: {restoredPerson.PhoneId}");
            }
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-SL034FT\SQLEXPRESS;Initial Catalog=mobilestoredb;Trusted_Connection=True;MultipleActiveResultSets=true";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Orders where OrderId = " + restoredPerson.OrderId + ";", cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                order.OrderId = dataReader.GetInt32(0);
                order.User = dataReader.GetString(1);
                order.Address = dataReader.GetString(2);
                order.ContactPhone = dataReader.GetString(3);
                order.PhoneId = dataReader.GetInt32(4);
            }
            NUnit.Framework.Assert.IsTrue(restoredPerson.Equals(order));
            cnn.Close();
        }


       
    }
}