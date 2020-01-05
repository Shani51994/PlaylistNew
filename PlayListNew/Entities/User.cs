using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.Entities
{
    // ronnnnn
    class User
    {
        private static User instance = null;

        private int id;
        private string email;
        private string password;
        private string fullName;
       
        private User()
        { }

        public static User Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
