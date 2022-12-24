using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class Membership {
        public string PlanName { protected set; get; }
        public Features Feature { protected set; get; }

        public Membership() {
            PlanName = "NULL";
            Feature = Features.None;
        }

        public virtual bool is_user_own(Account account) {
            return account.Features == Feature;
        }
    }
}
