using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Entities
{
    public class Session : BaseEntity
    {
        [ForeignKey(nameof(SessionStatus))]
        public int StationStatusId { get; set; }
        public SessionStatus SessionStatus { get; set; }

        [MaxLength(50)]
        public string EvId { get; set; }
        public DateTime StartedAt { get; set; }
        public int InitialSOC { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int? AvgPower { get; set; }
        public int? CustomedEnergy { get; set; }
        public int? FeedbackRate { get; set; }



        [ForeignKey(nameof(ChargeStation))]
        public int ChargeStationId { get; set; }
        public ChargeStation ChargeStation { get; set; }
    }
}
