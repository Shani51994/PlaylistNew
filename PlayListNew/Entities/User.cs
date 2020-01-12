using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.Entities
{
    class User
    {
        private static User instance = null;

        private int id;
        private string email;
       
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
