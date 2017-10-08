using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlanAzureFunctionApp.Models
{
    public class QueryPlan
    {
        public string PlanId { get; set; }
        public string ProductId { get; set; }
        public string Email { get; set; }
        public string Plan { get; set; }
    }
}
