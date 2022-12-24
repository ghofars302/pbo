using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class EnterpriseMembership : Membership {
        public EnterpriseMembership() {
            Feature = Features.EnterpriseMembership;
            PlanName = "Enterprise Membership";
        }
    }
}
