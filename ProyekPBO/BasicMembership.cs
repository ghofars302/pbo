using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class BasicMembership : Membership {
        public BasicMembership() {
            Feature = Features.BasicMembership;
            PlanName = "Basic Membership";
        }
    }
}
