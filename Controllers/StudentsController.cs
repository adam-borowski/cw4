using Cw4.DAL;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {
        private readonly IDbService _dbService;
        private string CONNECTION_STRING = "Server=localhost;Database=testDB;User Id=s18164;Password=123ASDqwe;";

        public StudentsController(IDbService dbService) {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent() {
            var list = new List<Student>();

            using(var connection = new SqlConnection(CONNECTION_STRING))
            using(var command = new SqlCommand()) {
                command.Connection = connection;
                command.CommandText = "SELECT s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester FROM Student s INNER JOIN Enrollment e ON e.IdEnrollment = s.IdEnrollment INNER JOIN Studies st ON st.IdStudy = e.IdStudy";
                
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    var student = new Student();
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    try {
                        student.BirthDate = DateTime.Parse(reader["BirthDate"].ToString()).Date;
                    } catch(FormatException) {
                        Console.WriteLine("Nie udało się przekonwertować daty.");
                    }
                    student.Name = reader["Name"].ToString();
                    student.Semester = Int32.Parse(reader["Semester"].ToString());
                    list.Add(student);
                }
                connection.Dispose();
            }


            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber) {
            var student = new Student();

            using(var connection = new SqlConnection(CONNECTION_STRING))
            using(var command = new SqlCommand()) {
                command.Connection = connection;
                command.CommandText = "SELECT s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester FROM Student s INNER JOIN Enrollment e ON e.IdEnrollment = s.IdEnrollment INNER JOIN Studies st ON st.IdStudy = e.IdStudy WHERE s.IndexNumber = @indexNumber";
                command.Parameters.AddWithValue("indexNumber", indexNumber);
                
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    try {
                        student.BirthDate = DateTime.Parse(reader["BirthDate"].ToString()).Date;
                    } catch(FormatException) {
                        Console.WriteLine("Nie udało się przekonwertować daty.");
                    }
                    student.Name = reader["Name"].ToString();
                    student.Semester = Int32.Parse(reader["Semester"].ToString());
                }
                connection.Dispose();
            }


            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student) {
            student.IndexNumber = $"s{new Random().Next(1,20000)}";
            return Ok(student);
        }

        [HttpPut]
        public IActionResult UpdateStudent(Student student) {
            return Ok("Aktualizacja dokonana.");
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id) {
            return Ok("Usuwanie ukończone.");
        }
    }
}
