using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api.Model
{
    public class RequestEquipment
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }

        public string Status { get; set; }
        public string Assignee { get; set; }
        public string Requestor { get; set; }
        public int Quantity { get; set; }

        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }
    }
}