namespace _01_ASPMVCTest.Areas._02_View.Models
{
    public class V_05_Person
    {
        public int PersonId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        //[UIHint("RoleTemplate")]
        public Role Role
        {
            get;
            set;
        }
    }

    public enum Role
    {
        Guest,
        User,
        Admin
    }
}