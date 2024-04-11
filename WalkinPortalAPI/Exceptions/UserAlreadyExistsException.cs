namespace WalkinPortalAPI.Exceptions
{
    public class UserAlreadyExistsException: Exception
    {
        public UserAlreadyExistsException() : base("User with giver email is already exists!") { }

        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
