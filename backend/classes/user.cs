Class User {
    private int Id { get; set; }
    private string Name { get; set; }
    private string Email { get; set; }
    private string Password { get; set; }

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