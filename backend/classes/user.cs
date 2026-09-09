namespace backend.classes
{
    public class User {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Password { get; private set; }


        //used for registering a new user
        public User(int id, string name, string email, string password) 
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        //used for retrieving a user from the database
        public User(int id, string name, string email) 
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public void SetName(string name) 
        {
            Name = name;
        }
        
        public void SetEmail(string email) 
        {
            Email = email;
        }

        public void SetPassword(string password) 
        {
            Password = password;
        }

    }
}


    

