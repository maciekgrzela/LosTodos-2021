using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class FacebookUserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public FacebookPictureData Picture { get; set; }
    }

    [NotMapped]
    public class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    [NotMapped]
    public class FacebookPicture
    {
        public string Url { get; set; }
    }
}