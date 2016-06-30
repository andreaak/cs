namespace CS_TDD._004_StubsAndMocks._018_Moq.Setup
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
