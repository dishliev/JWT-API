namespace JWT_API.Helpers
{
    public class PasswordHelper
    {
        public bool IsValidPassword(string userName, string password, string confirmPassword, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (userName.Length < 5)
            {
                errorMessage = "Invalid Username!";
                return false;
            }
            if (userName == password)
            {
                errorMessage = "Username and Password cannot match!";
                return false;
            }
            if (password != confirmPassword)
            {
                errorMessage = "Passwords do not match!";
                return false;
            }

            return true;
        }
    }
}
