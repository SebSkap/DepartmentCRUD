using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DepartmentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DepartmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string sqlDataSource;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("DepartmentConnectionString");
        }

        // GET: api/Department
        [HttpGet]
        public List<Department> Get()
        {
            string selectQuery = "SELECT id, [name] FROM dbo.department";
            List<Department> departments = new List<Department>();

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    while(sqlDataReader.Read())
                    {
                        Department dep = new Department();
                        dep.Id = sqlDataReader.GetInt32("id");
                        dep.Name = sqlDataReader.GetString("name");

                        departments.Add(dep);
                    }
                }
            }

            return departments;
        }

        // GET api/Department/5
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            string selectQuery = "SELECT id, [name] FROM dbo.department WHERE id = " + id;

            Department dep = new Department();

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        dep.Id = sqlDataReader.GetInt32("id");
                        dep.Name = sqlDataReader.GetString("name");
                    }
                }
            }

            return dep;
        }

        // POST api/Department
        [HttpPost]
        public void Post(Department department)
        {
            string selectQuery = @"INSERT INTO dbo.department VALUES ('" + department.Name + @"')";

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // PUT api/Department
        [HttpPut]
        public void Put(Department department)
        {
            string selectQuery = @"UPDATE dbo.department SET name = '" + department.Name + @"' WHERE id = '" + department.Id + "'";

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE api/Department/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string selectQuery = @"DELETE FROM dbo.department WHERE id = '" + id + "'";

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
