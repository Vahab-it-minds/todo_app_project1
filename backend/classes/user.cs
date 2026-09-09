namespace backend.classes
{
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(int id, string name, string email, string password) {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public int GetId() {
            return Id;
        }

        public string GetName() {
            return Name;
        }

        public string GetEmail() {
            return Email;
        }

        public string GetPassword() {
            return Password;
        }

        public void SetName(string name) {
            Name = name;
        }

        public void SetEmail(string email) {
            Email = email;
        }

        public void SetPassword(string password) {
            Password = password;
        }
    }
}


    

