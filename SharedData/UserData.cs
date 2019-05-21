using System;

namespace SharedData
{
    public class UserData
    {
        public UserData()
        {

        }

        public UserData(string name, string email, string message)
        {
            Name = name;
            Email = email;
            Message = message;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
