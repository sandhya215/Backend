using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;
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
    public class SignUpController : ControllerBase
    {
        private IUsersRepository UsersRepository;
        private ITeamMembersRepository TeamMembersRepository;
        public SignUpController(IUsersRepository UsersRepository, ITeamMembersRepository TeamMembersRepository)
        {
            this.UsersRepository = UsersRepository;
            this.TeamMembersRepository = TeamMembersRepository;     
         }
        //Signing up as user
        [HttpPost]
        [Route("UsersSignUp")]
        public IActionResult UsersSignUp(Users user, Role role)
        {
            FeedBack feedback = UsersRepository.addUsers(user, role);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return BadRequest(feedback.Message); }
        }

        //Signing up as TeamMember
        [HttpPost]
        [Route("TeamMemberSignUp")]
        public IActionResult supplierSignUp(TeamMembers teammembers, Role role)
        {
            FeedBack feedback = TeamMembersRepository.addTeamMembers(teammembers, role);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return BadRequest(feedback.Message); }
        }    

   }
}