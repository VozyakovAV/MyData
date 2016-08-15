using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}