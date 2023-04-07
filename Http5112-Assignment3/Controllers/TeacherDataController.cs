using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Http5112_Assignment3.Models;
using MySql.Data.MySqlClient;


namespace Http5112_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext Teacher = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //create an instance of a connection
            MySqlConnection Conn = Teacher.AccessDatabase();


            //open the connection between the web server and database
            Conn.Open();


            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "SELECT * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or concat(teacherfname, ' ', teacherlname) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            //get result set of query into a variable 
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            List<Teacher> Teachers = new List<Teacher>();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];


                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.EmployeeNumber = EmployeeNumber;
               
               




                //add the teacher name to the list
                Teachers.Add(newTeacher);
            }
            //close the connection between the sql database and web server
            Conn.Close();

            //return the final list of teacher names
            return Teachers;




        }
            [HttpGet]
            public Teacher FindTeacher(int id)
            {
                Teacher newTeacher = new Teacher();
            //create an instance of a connection
            MySqlConnection Conn = Teacher.AccessDatabase();


            //open the connection between the web server and database
            Conn.Open();


            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "SELECT * FROM teachers where teacherid = "+id;

           
            //get result set of query into a variable 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];

                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.EmployeeNumber = EmployeeNumber;


            }
    


            return newTeacher;
            }

    }
}
