namespace RWS.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestMessage.Response")]
    public partial class Response
    {
        public long ResponseID { get; set; }

        public short SourceTypeID { get; set; }

        public int SourceID { get; set; }

        public byte ProcessStatusID { get; set; }

        public string ResponseDescription { get; set; }

        public string ResponseMessage { get; set; }

        public short? RetryCount { get; set; }

        public DateTime? TimeStamp { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedByUsername { get; set; }

        public DateTime CreateDateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string LastUpdatedByUsername { get; set; }

        [StringLength(100)]
        public string LastUpdatedByLogin { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}
