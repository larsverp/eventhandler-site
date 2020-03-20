namespace RockStar_IT_Events.Models
{
    public class Guest
    {
        public string FirstName { get; private set; }

        public string Insertion { get; private set; }

        public string LastName { get; private set; }

        public bool GetMails { get; private set; }

        public Guest(string firstName, string insertion, string lastName, string email, string postalCode) : base(email, postalCode)
        {
            FirstName = firstName;
            Insertion = insertion;
            LastName = lastName;
        }

        public void ChangeFirstName(string newFirstName)
        {
            FirstName = newFirstName;
        }

        public void ChangeInsertions(string newInsertions)
        {
            Insertion = newInsertions;
        }

        public void ChangeLastName(string newLastName)
        {
            LastName = newLastName;
        }

        public void SetGetMail(bool value)
        {
            GetMails = value;
        }

        public void SetGetMail()
        {
            GetMails = true;
        }

        public void SetGetNoMail()
        {
            GetMails = false;
        }
    }
}
