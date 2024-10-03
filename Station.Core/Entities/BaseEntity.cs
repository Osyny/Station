using System.ComponentModel.DataAnnotations;

namespace Station.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public BaseEntity()
        {
            
        }
    }
}
