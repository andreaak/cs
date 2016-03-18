using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    class EmpPfDetails
    {
        private IEmpPersonalDetails empPersonalDetails;

        public EmpPfDetails(IEmpPersonalDetails empPersonalDetails)
        {
            this.empPersonalDetails = empPersonalDetails;
        }

        public float GetPfEmployerControlSofar(int empId)
        {
            int totalDuration = empPersonalDetails.GetDurationWorked(empId);
            float salary = empPersonalDetails.GetEmployeeSalary(empId);
            return 3456;
        }

        public bool IsPfEligible(int empId)
        {
            return empPersonalDetails.GetEmployeeSalary(empId) >= 4000;
        }
    }
}
