using LabII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LabII.DTOs
{
    public class UserPostModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }

        public static User ToUser(UserPostModel user)
        {
            UserRole role = Models.UserRole.Regular;

            if (user.UserRole == "UserManager")
            {
                role = Models.UserRole.UserManager;
            }
            else if (user.UserRole == "Admin")
            {
                role = Models.UserRole.Admin;
            }

            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Password = ComputeSha256Hash(user.Password),
                UserRole = role
            };
        }
        private static string ComputeSha256Hash(string password)
        {
            // Create a SHA256   
            // TODO: also use salt
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
