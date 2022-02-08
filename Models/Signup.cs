using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace KanbanBoard.Models
{
    public class Signup
      {
           [Key]
           public int signup_id{get;set;}
        
        public string email_id{get;set;}
         public string first_name { get; set; }
        public string last_name{get;set;}
        
        public long phone_number{get;set;}
        public string password{get;set;}
       // [ForeignKey("password")]

  //  public Login Login{get;set;}
         }
}