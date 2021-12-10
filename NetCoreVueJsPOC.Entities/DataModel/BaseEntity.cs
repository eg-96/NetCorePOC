using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreVueJsPOC.Entities.DataModel
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
