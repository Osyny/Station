namespace Station.Web.Controllers.ChargeStations.Dtos
{
    public class DataInput
    {
        public int Rows { get; set; }
        public int Skip { get; set; }
        //public int Take { get; set; }    
        public string? Sorting { get; set; }
        public string? FilterText { get; set; }
        public int? FilterOwnerId { get; set; }
    }
}
