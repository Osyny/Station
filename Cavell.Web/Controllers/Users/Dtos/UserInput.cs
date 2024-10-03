namespace Station.Web.Controllers.Users.Dtos
{
    public class UserInput
    {
        public int Rows { get; set; }    
        public int Skip { get; set; }    
        //public int Take { get; set; }    
        public string? Sorting { get; set; }    
        public string? FilterText { get; set; }
    }
}
