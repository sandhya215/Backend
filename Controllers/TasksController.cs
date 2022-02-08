// using Microsoft.AspNetCore.Mvc;
// using KanbanBoard.Models;
// using System.Collections.Generic;
// using KanbanBoard.Repositories;
// using KanbanBoard.Model;
// using Microsoft.AspNetCore.Authorization;

// namespace KanbanBoard.Controllers
// {
    
//   [Route("api/[controller]")]
//     [ApiController]
//     [Authorize(Roles = "USERS")]
//     public class UsersController : ControllerBase
//     {
//         private IRepository repository;
//         public UsersController(IRepository repository)
//         {
//             this.repository = repository;

//         }

//         //Change Password Module for User
//         [HttpPost]
//         [Route("changePassword")]
//         public IActionResult changePassword(string Email, ChangePasswordDTO changePasswordDTO)
//         {
//             FeedBack feedback = repository.changePassword(Email, changePasswordDTO);
//             if (feedback.Result == true) { return Ok(feedback.Message); }
//             else { return NotFound(feedback.Message); }

//         }

//         //Get All Complaints
//         [HttpGet]
//         [Route("getComplaints")]
//         public List<Complaint> getComplaints()
//         {
//             return repository.getComplaints();
//         }

//         //Add New Project
//         [HttpPost]
//         [Route("addProject")]
//         public string addProject(Project project)
//         {
//             repository.addProject(project);
//             return "Project Added.";
//         }

//         //Get All Project details
//         [HttpGet]
//         [Route("getProjectDetails")]
//         public List<Project> getProjects()
//         {
//             return repository.getProjects();
//         }

//         //Add New Task Details
//         [HttpPost]
//         [Route("addTaskDetails")]
//         public string addTaskDetails(Tasks tasks, TaskStatus status)
//         {
//             repository.addTaskDetails(tasks, status);
//             return "Tasks Added.";
//         }

//         //Delete project details
//         [HttpDelete]
//         [Route("deleteProjectDetails/{project_id}")]
//         public IActionResult deleteProjectDetails(long project_id)
//         {
//             try
//             {
//                 repository.deleteProjectDetails(project_id);
//                 return Ok("Project details Deleted");
//             }
//             catch (Exception)
//             {
//                 return BadRequest("Project Id Not Found");
//             }
//         }

//     }
// }
