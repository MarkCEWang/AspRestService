using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace EmployeeService.Controllers
{
    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        // GET: api/Course
        public IEnumerable<CourseTable> Get()
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                return entities.CourseTables.ToList();
            }
        }

        // GET: api/Course/5
        public CourseTable Get(int id)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                return entities.CourseTables.Find(id);
            }
        }

        [HttpGet]
        [Route("{id:int}/Employee")]
        public EmployeeData GetEmployeeByInstructor(int id)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                var course = entities.CourseTables.ToList().FirstOrDefault(e => e.CourseID == id);

                if (course != null && course.InstructorID != null)
                {
                    EmployeeController ec = new EmployeeController();
                    return ec.Get(course.InstructorID.Value);
                }
                return null;
            }
        }

        // POST: api/Course
        public CourseTable Post([FromBody]CourseTable value)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                value.Instructor = findInstructor(value.InstructorID);
                entities.CourseTables.Attach(value);

                entities.Entry(value).State = System.Data.Entity.EntityState.Unchanged;
                entities.SaveChanges();
                var return_val =  entities.CourseTables.Find(value.CourseID);
                return value;
            }
        }

        // PUT: api/Course/5
        public CourseTable Put(int id, [FromBody]CourseTable value)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                var curEntity = entities.CourseTables.ToList().FirstOrDefault(e => e.CourseID == id);

                if (curEntity != null)
                {
                    curEntity.CourseName = value.CourseName;
                    curEntity.InstructorID = value.InstructorID;
                    curEntity.Instructor = findInstructor(value.InstructorID);
                    curEntity.StudentIdList = value.StudentIdList;
                    curEntity.Schedule = value.Schedule;
                    curEntity.LengthOfCourse = value.LengthOfCourse;

                    entities.SaveChanges();
                    return value;
                }
                return null;

                
            }
        }

        public void changePartial(int id, string instructor)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                var curEntity = entities.CourseTables.ToList().FirstOrDefault(e => e.CourseID == id);

                if (curEntity != null)
                {
                    curEntity.Instructor = instructor;

                    entities.SaveChanges();
                }
            }
        }

        // DELETE: api/Course/5
        public void Delete(int id)
        {
            using (database_for_practiceEntities entities = new database_for_practiceEntities())
            {
                var toDelete = new CourseTable { CourseID = id };
                entities.CourseTables.Attach(toDelete);
                if (toDelete != null)
                {
                    entities.CourseTables.Remove(toDelete);
                    entities.SaveChanges();
                }
                
            }
        }

        private string findInstructor(int? id)
        {
            EmployeeController ec = new EmployeeController();
            var emp = ec.Get(id.Value);
            return emp.FirstName + emp.LastName[0];
        }

    }
}
