using System.ComponentModel.DataAnnotations;
using Zephyr.Domain;

namespace NHAutoMvcDemo.DomainModels
{
    public class Tag : Entity
    {
        [Required]
        public virtual string Name { get; set; }
    }
}