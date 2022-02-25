using System;
using Microsoft.AspNetCore.Identity;
using web_api.Authentication;

namespace web_api.Model
{
    public class RequestRoom
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string RequestorId { get; set; }
        public int RoomId { get; set; }
        public string AssigneeId { get; set; }


        public Room Room { get; set; }
        public ApplicationUser Requestor { get; set; }
        public ApplicationUser Assignee { get; set; }
    }
}