using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ForeSpark.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForeSpark.Installations
{
    public class Installations : Entity<int>, IFullAudited<User>
    {
        public string Make { get; set; }
        public string Serial { get; set; }
        public Cities.Cities City { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public int Status { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
        public User CreatorUser { get; set; }
        public User LastModifierUser { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public User DeleterUser { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
