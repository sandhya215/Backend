using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace KanbanBoard.Models
{
     public class Project
     {
          [Key] 
        public int project_id { get; set; }
        public string project_name { get; set; }
        public string ProjectStatus { get; set; }
    //     public int task_id{get;set;}
    //     [ForeignKey("task_id")]

    // public Tasks Tasks{get;set;}
 
  }

}