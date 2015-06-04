using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPOWeb.Models
{
    public class BaseViewModel
    {
        [ScaffoldColumn(false)]
        public int PlantId { get; set; }

        [DisplayName("Plant")]
        public string PlantName { get; set; }

    }
}