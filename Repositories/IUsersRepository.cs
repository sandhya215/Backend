using System.Collections.Generic;
//using System.Threading.Tasks;
using KanbanBoard.Models;
using KanbanBoard.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using KanbanBoard.DTO;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace KanbanBoard.Repositories
{
    public interface IUsersRepository
    {
       //Method for Viewing Users Details by id
      UsersDTO viewUserById(long user_id);

        //Method for adding New user
        FeedBack addUsers(Users users, Role role);

        //Change Password moethod for User
        FeedBack changePassword(string Email, ChangePasswordDTO changePasswordDTO);

        //Forgot Password moethod for Farmer
        FeedBack forgetPassword(string Email, ForgetPasswordDTO forgetPasswordDTO);

        //Method for Checking user Credentials with database
        Users validateUsers(Login login);

        //Method for Getting all tasks
        List<Tasks> getTasks();

        //Method for Adding tasks to database
        void addTasks(Tasks tasks, TaskStatus status);

        //Method for Getting all getProjects details
        List<Project> getProject();

        //Method for Adding Project details to database
        void addProjectDetails(Project project);

        //Method for Deleting Project Details from database
        void deleteProjectDetails(long project_id);

        //Method for Getting all tasks
        //List<Tasks> getTasks();

        //Method for Updating Project Status
        FeedBack updateTasksStatus(long task_id, TaskStatus status);

        
    }
}
