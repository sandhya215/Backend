using KanbanBoard.DTO;
using KanbanBoard.Models;
using KanbanBoard.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "USER")]
    public class UsersController : ControllerBase
    {
        private IUsersRepository repository;
        public UsersController(IUsersRepository repository)
        {
            this.repository = repository;

        }

        //Change Password Module for user
        [HttpPost]
        [Route("changePassword")]
        public IActionResult changePassword(string Email, ChangePasswordDTO changePasswordDTO)
        {
            FeedBack feedback = repository.changePassword(Email, changePasswordDTO);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return NotFound(feedback.Message); }

        }

        //Get All getTasks
        [HttpGet]
        [Route("getTasks")]
        public List<Tasks> getTasks()
        {
            return repository.getTasks();
        }

        //Add New Tasks
        [HttpPost]
        [Route("addTask")]
        public string addTasks(Tasks tasks, TaskStatus status)
        {
            repository.addTasks(tasks, status);
            return "Tasks Added.";
        }

        //Get All Project details
        [HttpGet]
        [Route("getProjectDetails")]
        public List<Project> getProject()
        {
            return repository.getProject();
        }

        //Add New Project Details
        [HttpPost]
        [Route("addProjectDetails")]
        public string addProjectDetails(Project project)
        {
            repository.addProjectDetails(project);
            return "project Added.";
        }

        //Delete ProjectDetails details
        [HttpDelete]
        [Route("deleteProjectDetails/{project_id}")]
        public IActionResult deleteProjectDetails(long Project_id)
        {
            try
            {
                repository.deleteProjectDetails(Project_id);
                return Ok("Project details Deleted");
            }
            catch (Exception)
            {
                return BadRequest("project Id Not Found");
            }
        }

        
        //Update Task Status
        [HttpPut]
        [Route("updateTasksStatus")]
        public IActionResult updateTasksStatus(int Task_id, TaskStatus status)
        {
            FeedBack feedback = repository.updateTasksStatus(Task_id, status);
            return Ok(feedback.Message);
        }
    }
}