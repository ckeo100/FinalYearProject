using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectClassLibrary.Model
{
    public class User 
    {
        public Guid UserUID { get; set; }
        public String FirstName { get; set; }
        public String SureName { get; set; }
        public String UserEmail { get; set; }
        public List<String> UserJobList { get; set; }
    }
    public class UserModel
    {
        public List<User> UserList = new List<User>();
        public Model.User user = new Model.User();
        
        public UserModel()
        {

            user.UserUID = Guid.NewGuid();
            user.FirstName = "Ciaran";
            user.SureName = "Keogh";
            user.UserEmail = "ciaranKeogh@Email.co.uk";
            user.UserJobList = new List<string>();

            UserList.Add(user);

        }
           
        public List<User> ShowAllUser()
        {
            //List<User> UserList = new List<User>();
            return UserList;

            
           
        }

        
    }

}
