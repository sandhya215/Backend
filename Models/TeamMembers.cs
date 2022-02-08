using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace KanbanBoard.Models
{
     public class TeamMembers
      {

        [Key]
         public int teammember_id { get; set; }
        public string teammemberName{get;set;}
        public string Role{get;set;}
        public string teammemberEmail{get;set;}
        public string TeamMembersStatus{get;set;}
        public string password{get;set;}
         
         public string teammemberAnswer{get;set;}

         

//         public int task_id{get;set;}
//         [ForeignKey("project_id")]

//     public Project Project{get;set;}
//     [ForeignKey("task_id")]

//     public Tasks Tasks{get;set;}
//          
}

}