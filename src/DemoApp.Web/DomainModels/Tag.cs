using System.ComponentModel.DataAnnotations;
using Zephyr.Domain;

namespace DemoApp.Web.DomainModels
{
    public class Tag
    {
        [Required]
        public virtual string Name { get; set; }
    }
}