using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    public interface IEmployeesDetails
    {

    }
    
    class EmployeesDetails : IEmployeesDetails
    {
        private IEmpPersonalDetails empPersonalDetails;

        public EmployeesDetails(IEmpPersonalDetails empPersonalDetails)
        {
            // TODO: Complete member initialization
            this.empPersonalDetails = empPersonalDetails;
        }

        public int GetHigherGradeEmployee(List<Employee> employees)
        {
            int count = 0;
            foreach (var item in employees)
            {
                var emp = empPersonalDetails.GetEmployeeDetails(item.EmpId);
                count++;
            }
            return count;
        }

        public int GetGratuityEligibleCount(List<Employee> employees)
        {
            int count = 0;
            foreach (var item in employees)
            {
                if (empPersonalDetails.GetDurationWorked(item.DurationWorked) > 30)
                {
                    count++;
                }
            }
            empPersonalDetails.GraduityEligibleCount = count;
            return count;
        }

        public int GetGratuityEligibleCount2(List<Employee> employees)
        {
            int count = 0;
            foreach (var item in employees)
            {
                if (empPersonalDetails.GetDurationWorked(item.DurationWorked) > 30)
                {
                    count++;
                }
            }
            empPersonalDetails.GraduityEligibleCount = count;
            return empPersonalDetails.GraduityEligibleCount;//count;
        }

        public string GetEmployeeValidEmailAddress(Employee employee)
        {
            if(empPersonalDetails.IsValidEmail(employee.Email))
            {
                return employee.Email;
            }
            return string.Empty;
        }
    }
}
