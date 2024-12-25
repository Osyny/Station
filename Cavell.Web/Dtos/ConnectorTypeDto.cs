using System.ComponentModel.DataAnnotations;

namespace Station.Web.Dtos
{
    public class ConnectorTypeDto : EntityDto
    {
        public string Name { get; set; }

        public string Details { get; set; }
    }
}
