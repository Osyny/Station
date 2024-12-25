namespace Station.Web.Dtos
{
    public class ConnectorUiStatusDto :  EntityDto
    {
        public string Name { get; set; }
        public int EnumValue { get; set; }

        public string Color { get; set; }
    }
}
