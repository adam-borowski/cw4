using Cw4.Models;
using System.Collections.Generic;

namespace Cw4.DAL {
    public interface IDbService {
        public IEnumerable<Student> GetStudents();
    }
}