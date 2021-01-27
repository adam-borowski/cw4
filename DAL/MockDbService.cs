using Cw4.Models;
using System.Collections.Generic;

namespace Cw4.DAL {
    public class MockDbService : IDbService {
        private static IEnumerable<Student> _students;

        static MockDbService() {
            _students = new List<Student>
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski", IndexNumber="s12345"},
                new Student{IdStudent=2, FirstName="Paweł", LastName="Nowak", IndexNumber="s11111"},
                new Student{IdStudent=3, FirstName="Adam", LastName="Borowski", IndexNumber="s18164"}
            };
        }

        public IEnumerable<Student> GetStudents() {
            return _students;
        }
    }
}