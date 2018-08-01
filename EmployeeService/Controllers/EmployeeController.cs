using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: api/Employee
        public IEnumerable<EmployeeData> Get()
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                return entities.EmployeeDatas.ToList();
            }
        }

        // GET: api/Employee/5
        public EmployeeData Get(int id)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                return entities.EmployeeDatas.ToList().FirstOrDefault(e => e.ID == id);
            }
        }

        // POST: api/Employee
        public void Post([FromBody]EmployeeData value)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                entities.EmployeeDatas.Add(value);
                entities.SaveChanges();
            }
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]EmployeeData value)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                var curEntity = entities.EmployeeDatas.ToList().FirstOrDefault(e => e.ID == id);

                curEntity.FirstName = value.FirstName;
                curEntity.LastName = value.LastName;
                curEntity.Gender = value.Gender;
                curEntity.Salary = value.Salary;

                entities.SaveChanges();
            }
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                entities.EmployeeDatas.Remove(entities.EmployeeDatas.ToList().FirstOrDefault(e => e.ID == id));
                entities.SaveChanges();
            }
        }
    }
}
