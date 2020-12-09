using System.Collections.Generic;

namespace ForeSpark.Request.Dto
{
    public class RequestDetailsDto
    {
        public int RequestId { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}
