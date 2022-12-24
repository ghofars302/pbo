using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class ProMembership : Membership {
        public ProMembership() {
            Feature = Features.ProMembership;
            PlanName = "Pro Membership";
        }
    }
}
