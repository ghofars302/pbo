using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class EnterpriseAccount : Account {
        public new static string ClassName = "Account";

        public EnterpriseAccount(int user_id, string username, string password, string email, string address) : base(user_id, username, password, email, address) {
        }

        public EnterpriseAccount(int user_id, string username, string email, string address, Payment payment) : base(user_id, username, email, address, payment) {
        }

        public EnterpriseAccount(int user_id, string username, string email, string address, Payment payment, int group) : base(user_id, username, email, address, payment, group) {
        }

        public override bool CheckSubscription() {
            DateTime now = DateTime.Now;
            DateTime expireAt = new DateTime(MembershipEndAt);

            TimeSpan span = expireAt - now;
            int days = span.Days;

            return days < -5;
        }

        public bool CheckSubscription(long time) {
            DateTime now = new DateTime(time);
            DateTime expireAt = new DateTime(MembershipEndAt);

            TimeSpan span = expireAt - now;
            int days = span.Days;
            
            return days < -5;
        }

        public new static string Sformat(string format, params object[] objects) {
            return Account.Sformat(format, objects);
        }
    }
}
