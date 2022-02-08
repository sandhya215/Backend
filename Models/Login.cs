using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace KanbanBoard.Models
{
    public class Login
    {
        //[Key]
        // public int login_id {get;set;}
        // public int signup_id{get;set;}
          [Key][Required]
          public string email_id {get;set;}        
           [Required]
        public string password {get;set;}
    //     [ForeignKey("signup_id")]
    //     public Signup Signup{get;set;}
    // 
    }
}