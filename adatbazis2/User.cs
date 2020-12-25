namespace adatbazis2
{
    public class User
    {
        int id;
        string name, password;
        public int Id
        {
            get { return id; } 
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; } 
        }
        private User()
        { 
            Id = 0; 
            Name = "UNDEFINED";
            Password = string.Empty;
        }
        public User(string name, string password) : this()
        { 
            Name = name; 
            Password = password;
        }
        public User(int id, string name) : this(name, string.Empty)
        {
            Id = id;
        }
    }
}
