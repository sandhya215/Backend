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
    public interface ITeamMembersRepository
    {
        //Method for viewing Supplier Details
        //TeamMembersDTO viewTeamMembersById(int teammember_id);

        //Method for adding New Supplier
        FeedBack addTeamMembers(TeamMembers teammembers, Role role);

        //Change Password moethod for Supplier
        FeedBack changePassword(string Email, ChangePasswordDTO changePasswordDTO);

        //Forget Password moethod for Supplier
        FeedBack forgetPassword(string Email, ForgetPasswordDTO forgetPasswordDTO);

        //Method for Checking TeamMembers Credentials with database
        TeamMembers validateTeamMembers(Login login);

        //Method for getting all crops
        List<Project> getProjects();
        // List<TeamMembers> getTeamMembers();


        //Method for Updating Crop Status
        FeedBack updateProjectStatus(long project_id, ProjectStatus status);
        //FeedBack TeamMembersStatus(long teammember_id, TeamMembersStatus status);
       // FeedBack TaskStatus(long task_id, TaskStatus status);



        //method for getting all Advertisements
        List<Tasks> getTasks();
        //List<TeamMembers> getTeamMembers();


        //Method for adding Advertisement details to database
        void addTasks(Tasks tasks, TaskStatus status);

        //Method for deleting Advertisement Details from database
         void TasksDetails(long task_id);
    }
}
