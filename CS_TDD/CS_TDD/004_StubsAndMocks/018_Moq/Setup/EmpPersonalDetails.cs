namespace CS_TDD._004_StubsAndMocks._018_Moq.Setup
{
    public interface IEmpPersonalDetails
    {
        float GetEmployeeSalary(int empId);

        object GetEmployeeDetails(int empId);

        int GraduityEligibleCount
        {
            get;
            set;
        }

        bool IsValidEmail(string email);

        int GetDurationWorked(int durationWorked);
    }

    public class EmpPersonalDetails : IEmpPersonalDetails
    {
        #region IEmpPersonalDetails Members

        public float GetEmployeeSalary(int empId)
        {
            return 2000;
        }

        public object GetEmployeeDetails(int empId)
        {
            return null;
        }

        public int GraduityEligibleCount
        {
            get;
            set;
        }

        public bool IsValidEmail(string email)
        {
            return true;
        }

        public int GetDurationWorked(int durationWorked)
        {
            return durationWorked * 2;
        }

        #endregion
    }
}
