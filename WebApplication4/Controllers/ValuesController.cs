using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace WebApplication4.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(@"Server=localhost;Database=TestEmployees;Trusted_Connection=True;");
        // GET api/values
        public string Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "no data found";
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Employee WHERE id = {id}", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "No data found";
            }
        }

        // POST api/values
        public string Post([FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand($"Insert into Employee(Name) VALUES('{ value }')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery(); //checks for successful insertion
            con.Close();
            if (i == 1)
            {
                return "Record inserted with the value as " + value;
            }
            else
            {
                return "Try again. No data inserted";
            }
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand($"UPDATE Employee SET Name = '{value}' WHERE id = '{id}'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i == 1)
            {
                return $"Record updated with value of {value} and id of {id}";
            }
            else
            {
                return "No record updated";
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
