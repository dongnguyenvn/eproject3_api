using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using web_api.Authentication;

namespace web_api.Model
{
    public class RequestRoom
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }

        public string Status { get; set; }
        public string Assignee { get; set; }
        public string Requestor { get; set; }

        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}