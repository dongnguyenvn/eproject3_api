namespace web_api.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }

        public RequestRoom RequestRoom { get; set; }
    }
}