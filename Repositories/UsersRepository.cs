using System.Collections.Generic;
//using System.Threading.Tasks;
using KanbanBoard.Models;
using KanbanBoard.Contexts;
using Microsoft.EntityFrameworkCore;
using KanbanBoard.DTO;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace KanbanBoard.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private ApplicationDbContext context;
        public UsersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Users validateUsers(Login login)
        {
            return context.Users.SingleOrDefault(u => u.userEmail == login.email_id && u.password == login.password);
        }

        //Method for adding New user
        public FeedBack addUsers(Users users, Role role)
        {
            FeedBack feedback = null;
            try
            {
                //Check if teammembers already exists by matching Email & taskId
                Users users1 = context.Users.SingleOrDefault(s => s.userEmail == users.userEmail);
                if (users1 == null)         
                {
                    
                 users.Role = role.ToString();
                    context.Users.Add(users);
                    context.SaveChanges();
                    feedback = new FeedBack() { Result = true, Message ="user Added" };
                }
                else
                {
                    feedback = new FeedBack() { Result = false, Message = "user with same EmailID already exists" };

                }

            }
            catch (Exception ex)
            {
                feedback = new FeedBack() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        // public FeedBack addUsers(Users users, Role role)
        // {
        //     FeedBack feedback = null;
        //     try
        //     {
        //         //Check if Farmer already exists by matching Email
        //         Users users1 = context.Users.SingleOrDefault(s => s.userEmail == users.userEmail);
        //         if (users1 == null)
        //         {
        //             //Add Farmers
        //             users1.Role = role.ToString();
        //             context.Users.Add(users1);
        //             context.SaveChanges();
        //             feedback = new FeedBack() { Result = true, Message = "users Added" };
        //         }
        //         else
        //         {
        //             feedback = new FeedBack() { Result = false, Message = "user with same EmailID already exists" };

        //         }

        //     }
        //     catch (Exception ex)
        //     {
        //         feedback = new FeedBack() { Result = false, Message = ex.Message };

        //     }
        //     return feedback;
        // }

        //Change Password moethod for User
        public FeedBack changePassword(string Email, ChangePasswordDTO changePasswordDTO)
        {
            Users user1 = context.Users.SingleOrDefault(s => s.userEmail == Email);
            if (user1 != null)
            {
                if (changePasswordDTO.oldPassword == user1.password)
                {
                    user1.password = changePasswordDTO.newPassword;
                    context.Users.Update(user1);
                    context.SaveChanges();
                    FeedBack feedback = new FeedBack { Result = true, Message = "Password Changed" };
                    return feedback;
                }
                else
                {
                    FeedBack feedback = new FeedBack { Result = false, Message = "Incorrect Password" };
                    return feedback;
                }
            }
            else
            {
                FeedBack feedback = new FeedBack { Result = false, Message = "user Email not registered!" };
                return feedback;
            }
        }

        //Forgot Password moethod for User
        public FeedBack forgetPassword(string Email, ForgetPasswordDTO forgetPasswordDTO)
        {
            Users user1 = context.Users.SingleOrDefault(s => s.userEmail == Email);
            if (user1 != null)
            {
                if (forgetPasswordDTO.answer == user1.userAnswer)
                {
                    user1.password = forgetPasswordDTO.newPassword;
                    context.Users.Update(user1);
                    context.SaveChanges();
                    FeedBack feedback = new FeedBack { Result = true, Message = "Password has been reset!" };
                    return feedback;
                }
                else
                {
                    FeedBack feedback = new FeedBack { Result = false, Message = "Incorrect Answer!" };
                    return feedback;
                }
            }
            else
            {
                FeedBack feedback = new FeedBack { Result = false, Message = "user Email not registered!" };
                return feedback;
            }
        }

        //Method for Checking User Credentials with database
        // public Users validateUsers(Login login)
        // {
        //     return context.Users.SingleOrDefault(u => u.userEmail == login.email_id && u.password == login.password);
        // }

        //Method for Viewing User Details by id
        public UsersDTO viewUserById(long user_id)
        {
            Users user = context.Users.SingleOrDefault(s => s.user_id == user_id); ;
            if (user != null)
            {
                UsersDTO UsersDTO = new UsersDTO();
                UsersDTO.user_id = user.user_id;
                UsersDTO.role = user.Role;
                UsersDTO.userName = user.userName;
                UsersDTO.userEmail = user.userEmail;
                // UsersDTO.userContanctNo = user.userContanctNo;
                // UsersDTO.userAddress = user.userAddress;
                if (UsersDTO != null)
                {
                    return UsersDTO;
                }
                else { return null; }
            }
            else { return null; }
        }

        //Method for Getting all Tasks
        public List<Tasks> getTasks()
        {
            try
            {
                List<Tasks> tasks = context.Tasks.FromSqlRaw("sp_GetTasks").ToList(); //implemented stored procedure
                return tasks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Method for Adding tasks to database
        public void addTasks(Tasks tasks, TaskStatus status)
        {
            tasks.TaskStatus = status.ToString();
            context.Tasks.Add(tasks);
            context.SaveChanges();
        }

        //Method for Getting all projects details
        public List<Project> getProject()
        {
            try
            {
                List<Project> project = context.Project.FromSqlRaw("sp_GetProject").ToList(); //implemented stored procedure
                return project;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Method for Adding Project details to database
        public void addProjectDetails(Project project)
        {
        
            context.Project.Add(project);
            context.SaveChanges();
        }

        //Method for Deleting project Details from database
        public void deleteProjectDetails(long project_id)
        {
            Project project = context.Project.SingleOrDefault(s => s.project_id == project_id);
            context.Project.Remove(project);
            context.SaveChanges();
        }


        //Method for Updating Tasks Status
        public FeedBack updateTasksStatus(long task_id, TaskStatus status)
        {
            Tasks tasks = context.Tasks.SingleOrDefault(s => s.task_id == task_id);
            if (tasks != null)
            {
                FeedBack feedback = new FeedBack();
                tasks.TaskStatus = status.ToString();
                context.Tasks.Update(tasks);
                context.SaveChanges();


                if (tasks.TaskStatus == "BACKLOG")
                {
                    feedback = new FeedBack { Result = true, Message = "un-completed task" };

                }
                else if (tasks.TaskStatus == "IN PROGRESS")
                {
                    feedback = new FeedBack { Result = true, Message = "task is in progress" };
                }
                else if (tasks.TaskStatus == "PEER REVIEW")
                {
                    feedback = new FeedBack { Result = true, Message = "task is in review" };
                }
                else
                {
                    feedback = new FeedBack { Result = true, Message = "done" };

                }
                return feedback;
            }
            else
            {
                FeedBack feedback = new FeedBack { Result = false, Message = "Invalid task_id" };
                return feedback;
            }
        
        }
    }
        
}
    



        
        