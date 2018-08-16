using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    [RoutePrefix("api/Employee")]
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

        [HttpGet]
        [Route("{id:int}/CourseTable")]
        public IEnumerable<CourseTable> GetCoursesOfEmp(int id)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                return entities.CourseTables.ToList().Where(e => e.InstructorID == id);
            }
        }

        // POST: api/Employee
        public EmployeeData Post([FromBody]EmployeeData value)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                entities.EmployeeDatas.Add(value);
                entities.SaveChanges();
                return value;
            }
        }

        // PUT: api/Employee/5
        public EmployeeData Put(int id, [FromBody]EmployeeData value)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                var curEntity = entities.EmployeeDatas.ToList().FirstOrDefault(e => e.ID == id);

                var courseEntity = entities.CourseTables.ToList().FirstOrDefault(e => e.InstructorID == id);

                CourseController cc = new CourseController();

                if (curEntity != null)
                {
                    curEntity.FirstName = value.FirstName;
                    curEntity.LastName = value.LastName;
                    curEntity.Gender = value.Gender;
                    curEntity.Salary = value.Salary;
                    cc.changePartial(courseEntity.CourseID, value.FirstName + value.LastName[0]);

                    entities.SaveChanges();
                    return value;
                }
                return null;
                
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
