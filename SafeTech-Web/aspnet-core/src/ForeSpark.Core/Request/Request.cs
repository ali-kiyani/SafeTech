using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ForeSpark.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForeSpark.Request
{
    public class Request : Entity<int>, IFullAudited<User>
    {
        public string CNIC { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Cities.Cities City { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public RequestStatus Status { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public User CreatorUser { get; set; }
        public User LastModifierUser { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public User DeleterUser { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public ICollection<RequestImages> ProductImages { get; set; }
    }
}
