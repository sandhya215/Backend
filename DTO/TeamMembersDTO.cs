namespace KanbanBoard.DTO
{
    public class TeamMembersDTO
    {
        public long teammember_id { get; set; } //Primary key
        public string role { get; set; }  //Role of the persons
        public string teammemberName { get; set; } 
        public string teammemberEmail { get; set; }  
        public string teammemberContanctNo { get; set; } 
        public string teammemberAddress { get; set; }  
            }
}
