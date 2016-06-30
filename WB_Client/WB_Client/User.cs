using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB_Client
{
    class User
    {
        private string login;
        private string password;
        public void setLogin(string value)
        {
            login = value;
        }
        public string getLogin()
        {
            return login;
        }
        public void setPassword(string value)
        {
            password = value;
        }
        public string getPassword()
        {
            return password;
        }
    }
}
