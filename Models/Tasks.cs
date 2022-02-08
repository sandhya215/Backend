using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace KanbanBoard.Models
{
     public class Tasks
     {
          [Key] 
        public int task_id { get; set; }
        public string task_name { get; set; }
        public string TaskStatus{ get; set; }

 
  }

}