using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;
using KanbanBoard.Contexts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using KanbanBoard.Repositories;
using KanbanBoard.DTO;
//using System.Threading.Tasks.TaskStatus;
using Microsoft.AspNetCore.Http;
//using KanbanBoard.Tasks.TaskStatus;
using System;

namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "TEAMMEMBERS")]
    public class TeamMembersController : ControllerBase
    {
        private ITeamMembersRepository repository;
        public TeamMembersController(ITeamMembersRepository repository)
        {
            this.repository = repository;

        }      

        //Change Password Module for TeamMembers
        [HttpPost]
        [Route("changePassword")]
        public IActionResult changePassword(string Email, ChangePasswordDTO changePasswordDTO)
        {
            FeedBack feedback = repository.changePassword(Email, changePasswordDTO);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return NotFound(feedback.Message); }

        }

        //Get All Project
        [HttpGet]
        [Route("getProjects")]
        public List<Project> getProjects()
        {
            return repository.getProjects();
        }

        //Update Status of Project details
        [HttpPut]
        [Route("updateProjectStatus")]
        public IActionResult updateProjectStatus(int project_id, ProjectStatus status)
        {
            FeedBack feedback = repository.updateProjectStatus(project_id, status);
            return Ok(feedback.Message);
        }

        //Get All Tasks
        [HttpGet]
        [Route("getTasks")]
        public List<Tasks> getTasks()
        {
            return repository.getTasks();
        }

        //Add New Tasks
        [HttpPost]
        [Route("addTasks")]
        public string addTasks(Tasks tasks, TaskStatus status)
        {
            repository.addTasks(tasks, status);
            return "Tasks Added.";
        }

        //Delete Tasks details
        [HttpDelete]
        [Route("TasksDetails/{task_id}")]
        public IActionResult TasksDetails(long task_id)
        {
            try
            {
                repository.TasksDetails(task_id);
                return Ok("Task details Deleted");
            }
            catch (Exception)
            {
                return BadRequest("Task Id Not Found");
            }
        }

    }
}
