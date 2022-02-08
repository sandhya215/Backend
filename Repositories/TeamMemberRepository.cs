using System.Collections.Generic;
//using System.Threading.Tasks;
using KanbanBoard.Models;
using KanbanBoard.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KanbanBoard.DTO;
using System;
// using Microsoft.AspNetCore.Http;
// using System.IO;
// using Microsoft.AspNetCore.Hosting;
// using KanbanBoard.Repositories;
namespace KanbanBoard.Repositories

{
    public class TeamMemberRepository : ITeamMembersRepository
    {
        private ApplicationDbContext context;
        public TeamMemberRepository(ApplicationDbContext Contexts)
        {
            this.context = Contexts;
        }

        //Method for adding New TeamMember
        public FeedBack addTeamMembers(TeamMembers teammember, Role role)
        {
            FeedBack feedback = null;
            try
            {
                //Check if teammembers already exists by matching Email & taskId
                  TeamMembers teammember2 = context.TeamMembers.SingleOrDefault(s => s.teammemberEmail == teammember.teammemberEmail);
                if (teammember2 == null)         
                {
                    
                    teammember.Role = role.ToString();
                    context.TeamMembers.Add(teammember);
                    context.SaveChanges();
                    feedback = new FeedBack() { Result = true, Message = "teammember Added" };
                }
                else
                {
                    feedback = new FeedBack() { Result = false, Message = "teammember with same EmailID already exists" };

                }

            }
            catch (Exception ex)
            {
                feedback = new FeedBack() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        //Change Password moethod for tm
        public FeedBack changePassword(string Email, ChangePasswordDTO changePasswordDTO)
        {
            TeamMembers teammember1 = context.TeamMembers.SingleOrDefault(s => s.teammemberEmail == Email);
            if (teammember1 != null)
            {
                if (changePasswordDTO.oldPassword == teammember1.password)
                {
                    teammember1.password = changePasswordDTO.newPassword;
                    context.TeamMembers.Update(teammember1);
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
                FeedBack feedback = new FeedBack { Result = false, Message = "TeamMember Email not registered!" };
                return feedback;
            }
        }

        //Forget Password moethod tm
        public FeedBack forgetPassword(string Email, ForgetPasswordDTO forgetPasswordDTO)
        {
            TeamMembers teammember1 = context.TeamMembers.SingleOrDefault(s => s.teammemberEmail == Email);
            if (teammember1 != null)
            {
                if (forgetPasswordDTO.answer == teammember1.teammemberAnswer)
                {
                    teammember1.password = forgetPasswordDTO.newPassword;
                    context.TeamMembers.Update(teammember1);
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
                FeedBack feedback = new FeedBack { Result = false, Message = "teammember Email not registered!" };
                return feedback;
            }
        }

        //Method for Checking tm Credentials with database
        public TeamMembers validateTeamMembers(Login login)
        {
            return context.TeamMembers.SingleOrDefault(u => u.teammemberEmail == login.email_id && u.password == login.password);
        }

        //Method for viewing teammember Details
        public TeamMembersDTO viewSupplierById(int teammember_id)
        {
            TeamMembers teammember = context.TeamMembers.SingleOrDefault(s => s.teammember_id == teammember_id); ;
            if (teammember != null)
            {
                TeamMembersDTO teamembersDTO = new TeamMembersDTO();
                teamembersDTO.teammember_id = teammember.teammember_id;
                teamembersDTO.role = teammember.Role;
                teamembersDTO.teammemberName = teammember.teammemberName;
                teamembersDTO.teammemberEmail = teammember.teammemberEmail;
                if (teamembersDTO != null)
                {
                    return teamembersDTO;
                }
                else { return null; }
            }
            else { return null; }
        }

        //Method for getting all Project
        public List<Project> getProjects()
        {
            try
            {
                List<Project> Project = context.Project.FromSqlRaw("sp_GetProject").ToList(); //implemented stored procedure
                return Project;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Method for Updating Project Status
        public FeedBack updateProjectStatus(long project_id, ProjectStatus status)
        {
            Project project = context.Project.SingleOrDefault(s => s.project_id == project_id);
            if (project != null)
            {
                FeedBack feedback = new FeedBack();
                project.ProjectStatus = status.ToString();
                context.Project.Update(project);
                context.SaveChanges();


                if (project.ProjectStatus == "UPDATED")
                {
                    feedback = new FeedBack { Result = true, Message = "Project updated" };

                }
                
                else
                {
                    feedback = new FeedBack { Result = true, Message = "Project deleted" };

                }
                return feedback;
            }
            else
            {
                FeedBack feedback = new FeedBack { Result = false, Message = "Invalid project_id" };
                return feedback;
            }
        }

        //Method for adding task details to database
        public void addTasks(Tasks tasks, TaskStatus status)
        {
            tasks.TaskStatus = status.ToString();
            context.Tasks.Add(tasks);
            context.SaveChanges();
        }

        //method for getting all tasks
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

        //Method for deleting task Details from database
        public void TasksDetails(long task_id)
        {
            Tasks tasks = context.Tasks.SingleOrDefault(s => s.task_id == task_id);
            context.Tasks.Remove(tasks);
            context.SaveChanges();
        }
    }
}
